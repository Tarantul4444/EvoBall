using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    void Start() {
        
    }

    public void LoadLevel(string levelScene) {
        string currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(levelScene, LoadSceneMode.Single);
    }
}
