using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Animator spikeTrapAnim; // Аниматор ловушки
    [SerializeField] private int damage = 1; // Урон, наносимый игроку
    [SerializeField] private float activationDelay = 0.5f; // Задержка перед активацией
    [SerializeField] private float activeDuration = 1f; // Время активности ловушки
    [SerializeField] private float closeDuration = 2f; // Задержка перед закрытием
    [SerializeField] private float damageInterval = 0.5f; // Интервал между нанесением урона

    private bool isActivated = false; // Флаг активности ловушки
    private float lastDamageTime; // Время последнего нанесения урона

    private void Start()
    {
        if (spikeTrapAnim == null)
        {
            spikeTrapAnim = GetComponent<Animator>();
        }

        StartCoroutine(TrapCycle());
    }

    private IEnumerator TrapCycle()
    {
        while (true)
        {
            spikeTrapAnim.SetTrigger("open");
            yield return new WaitForSeconds(activationDelay);

            isActivated = true; // Ловушка активна
            lastDamageTime = 0; // Сбрасываем таймер урона
            Debug.Log("Ловушка активировалась!");

            yield return new WaitForSeconds(activeDuration);

            isActivated = false; // Ловушка неактивна
            spikeTrapAnim.SetTrigger("close");
            Debug.Log("Ловушка деактивировалась!");

            yield return new WaitForSeconds(closeDuration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActivated)
        {
            DealDamage(collision); // Урон при входе
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isActivated && Time.time >= lastDamageTime + damageInterval)
        {
            DealDamage(collision); // Урон при нахождении
        }
    }

    private void DealDamage(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponentInParent<PlayerController>();
            if (playerController != null)
            {
                playerController.getDamage(); // Наносим урон игроку
                lastDamageTime = Time.time; // Обновляем время последнего урона
                Debug.Log("Ловушка нанесла урон игроку!");
            }
            else
            {
                Debug.LogWarning("PlayerController не найден на объекте игрока!");
            }
        }
    }
}
