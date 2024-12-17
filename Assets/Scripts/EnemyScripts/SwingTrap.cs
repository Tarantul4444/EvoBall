using UnityEngine;

public class SwingTrap : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Тег игрока

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что столкнувшийся объект имеет тег "Player"
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Игрок задет лезвием! Мгновенная смерть.");

            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.Die(); // Мгновенная смерть игрока
            }
            else
            {
                Debug.LogWarning("PlayerController не найден на объекте игрока!");
            }
        }
    }
}
