using UnityEngine;

public class FullscreenManager : MonoBehaviour {
    [SerializeField] private GameObject _FullScreenButton;
    [SerializeField] private GameObject _UnScreenButton;

    private const string FullScreenKey = "IsFullScreen";

    private void Start() {
        bool isFullScreen = PlayerPrefs.GetInt(FullScreenKey, 0) == 1;
        Screen.fullScreen = isFullScreen;
        UpdateButtonsState(isFullScreen);
    }

    public void EnableFullScreen() {
        Screen.fullScreen = true;
        SaveFullScreenState(true);
        UpdateButtonsState(true);
    }

    public void DisableFullScreen() {
        Screen.fullScreen = false;
        SaveFullScreenState(false);
        UpdateButtonsState(false);
    }

    private void SaveFullScreenState(bool isFullScreen) {
        PlayerPrefs.SetInt(FullScreenKey, isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateButtonsState(bool isFullScreen) {
        _FullScreenButton.SetActive(!isFullScreen);
        _UnScreenButton.SetActive(isFullScreen);
    }
}
