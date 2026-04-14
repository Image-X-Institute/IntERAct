import pyrealsense2 as rs
import numpy as np

# class DepthCamera:
#     def __init__(self,
#                  depth_res=(1024, 768),
#                  color_res=(1280, 720),
#                  fps=30,
#                  l515_preset='max_range',   # 'default','short_range','max_range','high_accuracy','low_ambient_light','high_density'
#                  laser_power=100.0,          # 0–100
#                  confidence=1.0):            # 0–10 (lower = more pixels accepted)

#         self.pipeline = rs.pipeline()
#         self.config = rs.config()

#         self.config.enable_stream(rs.stream.depth, depth_res[0], depth_res[1], rs.format.z16, fps)
#         self.config.enable_stream(rs.stream.color, color_res[0], color_res[1], rs.format.bgr8, fps)

#         self.profile = self.pipeline.start(self.config)

#         dev = self.profile.get_device()
#         self.depth_sensor = dev.first_depth_sensor()
#         self.depth_scale = self.depth_sensor.get_depth_scale()  # meters per unit

#         # SAFE: only set visual preset if the attribute exists
#         try:
#             if device.get_info(rs.camera_info.product_line) == 'L500':
#                 # Try to use 'max_range' (widely available); fall back to 'default'
#                 vp = getattr(rs.l500_visual_preset, 'max_range', None)
#                 if vp is None:
#                     vp = getattr(rs.l500_visual_preset, 'default', None)
#                 if vp is not None:
#                     self.pipeline.get_active_profile().get_device().first_depth_sensor()\
#                         .set_option(rs.option.visual_preset, float(vp))
#         except Exception:
#             pass

#         # # If this is an L500 (L515) device, set up useful options
#         # if dev.get_info(rs.camera_info.product_line) == 'L500':
#         #     # Select a visual preset
#         #     preset_map = {
#         #         'default': rs.l500_visual_preset.default,
#         #         'short_range': rs.l500_visual_preset.short_range,
#         #         'max_range': rs.l500_visual_preset.max_range,
#         #         'high_accuracy': rs.l500_visual_preset.high_accuracy,
#         #         'low_ambient_light': rs.l500_visual_preset.low_ambient_light,
#         #         'high_density': rs.l500_visual_preset.high_density,
#         #     }
#         #     self.depth_sensor.set_option(rs.option.visual_preset,
#         #                                  preset_map.get(l515_preset, rs.l500_visual_preset.max_range))
#         #     # Ensure laser is on and confidence is permissive
#         #     if self.depth_sensor.supports(rs.option.laser_power):
#         #         self.depth_sensor.set_option(rs.option.laser_power, float(laser_power))
#         #     if self.depth_sensor.supports(rs.option.confidence_threshold):
#         #         self.depth_sensor.set_option(rs.option.confidence_threshold, float(confidence))

#         # Do the alignment once and reuse
#         self.align = rs.align(rs.stream.color)

#         # Optional: reduce holes
#         self.hole_filling = rs.hole_filling_filter(2)  # 0..2

#     def get_frame(self):
#         frames = self.pipeline.wait_for_frames()
#         frames = self.align.process(frames)

#         depth_frame = frames.get_depth_frame()
#         color_frame = frames.get_color_frame()

#         if not depth_frame or not color_frame:
#             return False, None, None

#         # Optional hole filling (works on depth frames)
#         depth_frame = self.hole_filling.process(depth_frame)

#         depth_image = np.asanyarray(depth_frame.get_data())  # uint16 (Z16)
#         color_image = np.asanyarray(color_frame.get_data())  # uint8 BGR

#         return True, depth_image, color_image

#     def release(self):
#         self.pipeline.stop()

# import numpy as np

# def clamp_roi(x, y, w, h, width, height):
#     x = max(0, min(x, width - 1))
#     y = max(0, min(y, height - 1))
#     w = max(1, min(w, width - x))
#     h = max(1, min(h, height - y))
#     return x, y, w, h

# def get_average_distance_mm(roi, depth_z16, depth_scale):
#     x, y, w, h = roi
#     H, W = depth_z16.shape
#     x, y, w, h = clamp_roi(x, y, w, h, W, H)

#     roi_depth = depth_z16[y:y+h, x:x+w]
#     # Valid pixels are > 0
#     valid = roi_depth[roi_depth > 0]
#     if valid.size == 0:
#         return None, 0.0  # No valid depth; also return valid ratio 0
#     distance_m = float(valid.mean()) * depth_scale
#     valid_ratio = valid.size / float(w*h)
#     return distance_m * 1000.0, valid_ratio  # mm, fraction of valid pixels


class DepthCamera:
    def __init__(self):
        # Configure depth and color streams
        self.pipeline = rs.pipeline()
        config = rs.config()
 
        # Get device product line for setting a supporting resolution
        pipeline_wrapper = rs.pipeline_wrapper(self.pipeline)
        pipeline_profile = config.resolve(pipeline_wrapper)
        device = pipeline_profile.get_device()
        device_product_line = str(device.get_info(rs.camera_info.product_line))
 
        # For L515 settings
        #config.enable_stream(rs.stream.depth, 1024, 768, rs.format.z16, 30)
        #config.enable_stream(rs.stream.color, 1280, 720, rs.format.bgr8, 30)

        # #For D415 settings
        config.enable_stream(rs.stream.depth, 640, 480, rs.format.z16, 30)
        config.enable_stream(rs.stream.color, 1280, 720, rs.format.bgr8, 30)
 
        # Align objects
        #align_to = rs.stream.color
        #align = rs.align(align_to)
 
        # Start streaming
        self.pipeline.start(config)

 
    def get_frame(self):
        #frames = self.pipeline.wait_for_frames()
        #depth_frame = frames.get_depth_frame()
        #color_frame = frames.get_color_frame()
 
        frames = self.pipeline.wait_for_frames()
        align_to = rs.stream.color
        align = rs.align(align_to)
        aligned_frames = align.process(frames)
        depth_frame = aligned_frames.get_depth_frame()
        color_frame = aligned_frames.get_color_frame()

        # temporal_filter = rs.temporal_filter()
        # temporal_filter.set_option(rs.option.filter_smooth_alpha, 0.5)  # Alpha blend factor
        # temporal_filter.set_option(rs.option.filter_smooth_delta, 50)   # Delta for replacement
        # temporal_filter.set_option(rs.option.holes_fill, 3)            # Fill holes
        
        # depth_frame = temporal_filter.process(depth_frame)
        depth_image = np.asanyarray(depth_frame.get_data())
        color_image = np.asanyarray(color_frame.get_data())
        if not depth_frame or not color_frame:
            return False, None, None
        return True, depth_image, color_image
 
    def release(self):
        self.pipeline.stop()