import librosa
import json
import matplotlib.pyplot as plt
import numpy as np
from scipy.signal import find_peaks

file_path = "H:\game jam 22 march\Shaolin-Slice\Assets\Music\Soundtrack\Dawn on the Mountains.mp3"

# Load the audio file
audio_data, sample_rate = librosa.load(file_path)

# Set hop length (adjustable for sensitivity)
hop_length = 400

# Compute onset envelope
onset_env = librosa.onset.onset_strength(y=audio_data, sr=sample_rate, hop_length=hop_length)

# Manually detect beats based on peaks
threshold = 4.75 # Adjust based on your song
peak_indices, _ = find_peaks(onset_env, height=threshold)  # Find peaks above threshold

# Convert peak indices to time
times = librosa.times_like(onset_env, sr=sample_rate, hop_length=hop_length)
beat_times = times[peak_indices]  # Extract beat timestamps

# Save to JSON file
output_path = "H:\\game jam 22 march\\Shaolin-Slice\\Assets\\BeatMaps\\0_beatmap.json"
beat_times_list = beat_times.tolist()

with open(output_path, "w") as f:
    json.dump({"beats": beat_times_list}, f)

print("Complete! Beats detected:", len(beat_times_list))

# Plot onset strength with detected beats
plt.figure(figsize=(10, 4))
plt.plot(times, onset_env, label="Onset Strength", color="b")
plt.vlines(beat_times, 0, max(onset_env), color="r", linestyle="--", label="Detected Beats")

plt.xlabel("Time (seconds)")
plt.xticks(np.arange(0, int(times[-1]) + 1, step=1))  # Ensure every second is labeled
plt.xlim([0, times[-1]])  



plt.legend()
plt.title("Detected Beats")
plt.show()
