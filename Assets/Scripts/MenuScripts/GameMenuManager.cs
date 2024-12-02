using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject SettingsMenu;

    private const string menuScene = "Menu";
    private bool isPaused = false;
    private bool isSettingsOpened = false;

    private void Start() {
        Time.timeScale = 1f;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            if (Input.GetKeyDown(KeyCode.H)) OpenMenu();
            if (Input.GetKeyDown(KeyCode.R)) RestartLevel();
            if (Input.GetKeyDown(KeyCode.P)) PauseLevel();
            if (Input.GetKeyDown(KeyCode.S)) OpenSettings();
        }
    }

    public void QuitGame() {
        Debug.Log("Game Exiited");
        Application.Quit();
    }

    public void OpenMenu() {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseLevel() {
        Time.timeScale = 1f - Time.timeScale;

        isPaused = !isPaused;
        PauseButton.SetActive(!isPaused);
        PlayButton.SetActive(isPaused);

        if (!isPaused && isSettingsOpened) {
            isSettingsOpened = false;
            SettingsMenu.SetActive(isSettingsOpened);
        }
    }

    public void OpenSettings() {
        if (!isSettingsOpened) {
            if (!isPaused) PauseLevel();
            isSettingsOpened = true;
            SettingsMenu.SetActive(isSettingsOpened);
        } else {
            isSettingsOpened = false;
            SettingsMenu.SetActive(isSettingsOpened);
            if (isPaused) PauseLevel();
        }
    }
}
