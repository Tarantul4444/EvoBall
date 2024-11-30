using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    private void Start() {
        
    }

    public void QuitGame() {
        Debug.Log("Game Exiited");
        Application.Quit();
    }
}
