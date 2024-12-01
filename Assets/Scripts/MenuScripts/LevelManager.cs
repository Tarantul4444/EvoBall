using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    void Start() {
        
    }

    public void LoadLevel(string levelScene) {
        SceneManager.LoadScene(levelScene, LoadSceneMode.Single);
    }
}
