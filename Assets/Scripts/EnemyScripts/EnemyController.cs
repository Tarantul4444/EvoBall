using UnityEngine;

public class EnemySpiderController : MonoBehaviour {
    [SerializeField] private Animator animator; // Ссылка на Animator
    [SerializeField] private int damage = 1; // Урон, наносимый игроку
    [SerializeField] private float moveSpeed = 3f; // Скорость движения
    [SerializeField] private Transform player; // Цель (игрок)
    [SerializeField] private int health = 1; // Количество здоровья

    private bool isDead = false; // Флаг смерти

    private void Start() {
        animator.SetBool("isWalking", false); // Убедимся, что анимация "Idle" активна
    }

    private void Update() {
        if (isDead) return; // Если паук мертв, ничего не делаем

        if (player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Включаем анимацию ходьбы
            animator.SetBool("isWalking", true);
        } else {
            // Выключаем анимацию ходьбы
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player")) {
            // Случайная атака
            string attackAnimation = Random.Range(0, 2) == 0 ? "Attack1" : "Attack2";
            animator.SetTrigger(attackAnimation); // Проигрываем случайную атаку
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.getDamage(); // Наносим урон игроку
            }
        }
    }

    public void TakeDamage() {
        if (isDead) return; // Если уже мертв, ничего не делаем

        health--; // Уменьшаем здоровье
        if (health <= 0) {
            isDead = true; // Устанавливаем флаг смерти
            animator.SetTrigger("Die"); // Проигрываем анимацию смерти
            Destroy(gameObject, 2f); // Удаляем объект через 2 секунды
        } else {
            animator.SetTrigger("TakeDamage"); // Проигрываем анимацию получения урона
        }
    }
}
