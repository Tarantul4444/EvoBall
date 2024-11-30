using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer audioMixer;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";

    void Start() {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float savedSoundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 1f);

        musicSlider.value = savedMusicVolume;
        soundSlider.value = savedSoundVolume;

        SetMusicVolume(savedMusicVolume);
        SetSoundVolume(savedSoundVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    private void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SaveVolume(MusicVolumeKey, volume);
    }

    private void SetSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
        SaveVolume(SoundVolumeKey, volume);
    }

    private void SaveVolume(string key, float volume) {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }
}
