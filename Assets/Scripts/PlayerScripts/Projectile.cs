using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float lifeTime = 3f;

    void Start() {
        Destroy(gameObject, lifeTime); // Уничтожить снаряд через заданное время
    }

    private void OnCollisionEnter(Collision collision) {
        // Проверяем, если попали во врага
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("isBoss")) {
            EnemySpiderController enemy = collision.gameObject.GetComponent<EnemySpiderController>();
            if (enemy != null) {
                enemy.TakeDamage(); // Наносим урон врагу
            }
            Destroy(gameObject); // Уничтожаем снаряд
        }
    }
}