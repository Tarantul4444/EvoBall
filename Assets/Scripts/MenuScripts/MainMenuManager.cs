using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    void Start() {
        
    }

    public void QuitGame() {
        Debug.Log("Game Exiited");
        Application.Quit();
    }
}
