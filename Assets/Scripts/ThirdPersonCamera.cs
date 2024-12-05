using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Цель для следования (мяч)
    public Transform target;

    // Смещение камеры относительно цели
    public Vector3 offset = new Vector3(5, 10, -5);

    // Скорость сглаживания движения камеры
    public float smoothSpeed = 0.125f;

    // Максимальные и минимальные углы наклона камеры
    public float maxXAngle = 80f;
    public float minXAngle = 20f;

    // Чувствительность мыши для вращения камеры
    public float rotationSpeed = 5f;

    // Внутренние переменные для хранения углов
    private float yaw = 0f;
    private float pitch = 30f;

    void Start()
    {
        // Инициализация начального смещения
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for ThirdPersonCamera script.");
            return;
        }

        // Получение ввода мыши для вращения камеры
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minXAngle, maxXAngle);

        // Создание поворота на основе углов
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Вычисление желаемой позиции камеры
        Vector3 desiredPosition = target.position + rotation * offset;

        // Плавное перемещение камеры к желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Направление камеры на цель
        transform.LookAt(target.position + Vector3.up * 1.5f); // Слегка выше центра мяча
    }
}

