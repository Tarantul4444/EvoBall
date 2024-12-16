using System.Collections;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    public Animator spikeTrapAnim; // Animator для ловушки
    public int damage = 1; // Урон, наносимый цели
    public float activationDelay = 0.5f; // Задержка перед нанесением урона
    public GameObject target; // Объект, которому будет наноситься урон
    private bool isActivated = false; // Флаг активации ловушки

    private void Awake() {
        // Получаем компонент Animator
        spikeTrapAnim = GetComponent<Animator>();
        // Запускаем цикл открытия/закрытия ловушки
        StartCoroutine(OpenCloseTrap());
    }

    private IEnumerator OpenCloseTrap() {
        while (true) {
            // Анимация открытия ловушки
            spikeTrapAnim.SetTrigger("open");
            yield return new WaitForSeconds(activationDelay);

            // Активируем урон
            isActivated = true;
            yield return new WaitForSeconds(1); // Время, когда ловушка активна

            // Деактивируем урон
            isActivated = false;

            // Анимация закрытия ловушки
            spikeTrapAnim.SetTrigger("close");
            yield return new WaitForSeconds(2);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Если ловушка активна и столкновение с целью
        if (isActivated && other.gameObject == target) {
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.getDamage(); // Наносим урон цели
                Debug.Log("Ловушка нанесла урон!");
            }
        }
    }
}
