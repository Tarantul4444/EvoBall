using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Стрела столкнулась с: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("Попадание в врага!");
            EnemySpiderController enemy = collision.gameObject.GetComponent<EnemySpiderController>();
            if (enemy != null) {
                Debug.Log("Вызов TakeDamage у паука!");
                enemy.TakeDamage();
            } else {
                Debug.LogWarning("EnemySpiderController не найден на объекте врага!");
            }
        }

        Destroy(gameObject); 
    }
}