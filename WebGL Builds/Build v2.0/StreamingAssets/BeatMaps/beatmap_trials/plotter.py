import matplotlib.pyplot as plt
import matplotlib.image as mpimg
import numpy as np

image_path = r"H:\game jam 22 march\Shaolin-Slice\Assets\BeatMaps\beatmap_trials\0_onsetmap.png"

# Load image
img = mpimg.imread(image_path)

# Display image
plt.figure(figsize=(12, 6))  # Adjust figure size
plt.imshow(img)

# Set x-axis labels for each second (assuming the width represents time)
total_seconds = 40  # Change this based on your audio duration
plt.xticks(np.linspace(0, img.shape[1], total_seconds + 1), np.arange(0, total_seconds + 1))

# Set labels
plt.xlabel("Time (seconds)")
plt.ylabel("Onset Strength")

plt.show()
