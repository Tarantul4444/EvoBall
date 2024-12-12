using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject[] starTemplatesLevel1;
    [SerializeField] private GameObject[] starsLevel1;
    [SerializeField] private GameObject[] starTemplatesLevel2;
    [SerializeField] private GameObject[] starsLevel2;
    [SerializeField] private GameObject[] starTemplatesLevel3;
    [SerializeField] private GameObject[] starsLevel3;

    private const string StarsLevel1 = "StarsLevel1";
    private const string StarsLevel2 = "StarsLevel2";
    private const string StarsLevel3 = "StarsLevel3";

    void Start() {
        depicStars(StarsLevel1, starTemplatesLevel1, starsLevel1);
        depicStars(StarsLevel2, starTemplatesLevel2, starsLevel2);
        depicStars(StarsLevel3, starTemplatesLevel3, starsLevel3);
    }

    private void depicStars(string starsLevel, GameObject[] starTemplates, GameObject[] stars) {
        string levelStars = PlayerPrefs.GetString(starsLevel, "000");

        for (int i = 0; i < starTemplates.Length; ++i) {
            if (levelStars[i] == '1') {
                stars[i].SetActive(true);
                starTemplates[i].SetActive(false);
            }
        }
    }

    public void LoadLevel(string levelScene) {
        SceneManager.LoadScene(levelScene, LoadSceneMode.Single);
    }
}
