using UnityEngine;

public class HeartsController : MonoBehaviour {
    [SerializeField] private GameObject[] hearts;

    private int currentHeartIndex = 2;

    void Start() {
        
    }

    public void RemoveHeart() {
        if (currentHeartIndex > -1) {
            hearts[currentHeartIndex].SetActive(false);
            --currentHeartIndex;
        }
    }
}
