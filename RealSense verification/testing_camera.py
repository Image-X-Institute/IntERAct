
import pyrealsense2 as rs

# Try modern stdlib first (Python 3.8+)
try:
    from importlib.metadata import version, PackageNotFoundError  # Python ≥3.8
except ImportError:
    # Fallback for older Python
    from importlib_metadata import version, PackageNotFoundError   # backport

try:
    pkg_ver = version("pyrealsense2")
except PackageNotFoundError:
    pkg_ver = "unknown (package not found via metadata)"

# Device info
ctx = rs.context()
devs = ctx.query_devices()
if not devs:
    print("No RealSense devices found")
else:
    dev = devs[0]
    name = dev.get_info(rs.camera_info.name)
    serial = dev.get_info(rs.camera_info.serial_number)
    fw = dev.get_info(rs.camera_info.firmware_version)
    print(f"Device: {name}, Serial: {serial}")
    print(f"Firmware: {fw}")
    print(f"pyrealsense2 (Python package) version: {pkg_ver}")


import pyrealsense2 as rs

pipeline = rs.pipeline()
config = rs.config()

# Lock to your D415 serial (recommended to avoid multi-device confusion)
config.enable_device("104122060696")

# Conservative, widely supported streams
config.enable_stream(rs.stream.depth, 640, 480, rs.format.z16, 30)
config.enable_stream(rs.stream.color, 640, 480, rs.format.rgb8, 30)  # use bgr8 if your code expects OpenCV BGR

try:
    profile = pipeline.start(config)
    print("Pipeline started OK")
    # ... read frames here ...
finally:
    pipeline.stop()
