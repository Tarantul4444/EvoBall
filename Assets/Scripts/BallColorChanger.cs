using UnityEngine;

public class BallColorChanger : MonoBehaviour
{
    // Массив цветов, между которыми будет происходить переключение
    public Color[] colors;

    // Индекс текущего цвета
    private int currentColorIndex = 0;

    // Ссылка на Renderer мяча
    private Renderer ballRenderer;

    void Start()
    {
        // Получаем компонент Renderer
        ballRenderer = GetComponent<Renderer>();

        // Устанавливаем начальный цвет, если массив не пуст
        if (colors.Length > 0)
        {
            ballRenderer.material.color = colors[currentColorIndex];
        }
    }

    void Update()
    {
        // Проверяем нажатие кнопки (например, клавиша C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (colors.Length == 0)
            return;

        // Увеличиваем индекс цвета и сбрасываем, если достигли конца массива
        currentColorIndex = (currentColorIndex + 1) % colors.Length;

        // Применяем новый цвет
        ballRenderer.material.color = colors[currentColorIndex];
    }
}
