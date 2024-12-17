using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1; // Количество урона
    [SerializeField] private string playerTag = "Player"; // Тег игрока для проверки

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что объект имеет тег Player
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Игрок коснулся ловушки!");

            // Попытка получить компонент PlayerController
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.getDamage(); // Наносим урон игроку
                Debug.Log($"Игрок получил урон: {damage} HP");
            }
            else
            {
                Debug.LogWarning("PlayerController не найден на объекте игрока!");
            }
        }
    }
}
