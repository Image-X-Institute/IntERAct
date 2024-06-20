import pyrealsense2 as rs
import numpy as np
 
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
        config.enable_stream(rs.stream.depth, 1024, 768, rs.format.z16, 30)
        config.enable_stream(rs.stream.color, 1280, 720, rs.format.bgr8, 30)

 
        # #For D415 settings
        # config.enable_stream(rs.stream.depth, 640, 480, rs.format.z16, 30)
        # config.enable_stream(rs.stream.color, 1280, 720, rs.format.bgr8, 30)
 
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