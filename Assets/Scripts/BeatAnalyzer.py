import librosa
import json

file_path = "H:\game jam 22 march\Shaolin-Slice\Assets\Music\Rise of the Tea Shop General.mp3"
audio_data , sample_rate = librosa.load(file_path)


onset_env = librosa.onset.onset_strength(y=audio_data , sr=sample_rate)


tempo , beat_frames = librosa.beat.beat_track(onset_envelope = onset_env , sr = sample_rate)

beat_times = librosa.frames_to_time(beat_frames , sr = sample_rate)



output_path = "H:\\game jam 22 march\\Shaolin-Slice\\Assets\Scripts\\beatmap_tutorial.json"

beat_times_list = beat_times.tolist()

with  open(output_path , "w") as f:
    json.dump({"beats": beat_times_list} , f)
    

print("Complete!")
    
    


