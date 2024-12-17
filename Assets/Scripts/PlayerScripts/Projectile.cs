using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; // Двигаем снаряд вперёд
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Точная коллизия
        Destroy(gameObject, lifeTime); // Уничтожаем снаряд по времени
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemySpiderController enemy = collision.gameObject.GetComponent<EnemySpiderController>();
            if (enemy != null) {
                enemy.TakeDamage(); // Наносим урон
            }
            Destroy(gameObject); // Уничтожаем снаряд
        }
    }
}
