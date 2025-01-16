using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }  // Синглтон для доступа из других скриптов

    public Transform target;  // Цель, за которой будет следовать камера (например, игрок)
    public float smoothSpeed = 0.125f;  // Скорость сглаживания
    public Vector3 offset;  // Смещение камеры относительно игрока

    // Переменные для тряски камеры
    private bool isShaking = false;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.2f;
    private float dampingSpeed = 1.0f;

    private Vector3 initialPosition;

    private void Awake()
    {
        // Реализация паттерна Singleton
        if (Instance == null)
        {
            Instance = this;
            initialPosition = transform.position;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;  // Желаемая позиция камеры
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Плавное следование
            smoothedPosition.z = -10;  // Фиксируем ось Z для 2D

            if (isShaking)
            {
                if (shakeDuration > 0)
                {
                    // Генерация случайного смещения внутри сферы
                    Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
                    shakeOffset.z = 0; // Избегаем смещения по оси Z
                    smoothedPosition += shakeOffset;

                    shakeDuration -= Time.fixedDeltaTime * dampingSpeed;
                }
                else
                {
                    isShaking = false;
                }
            }

            transform.position = smoothedPosition;
        }
    }

    /// <summary>
    /// Метод для запуска тряски камеры.
    /// </summary>
    /// <param name="duration">Длительность тряски в секундах.</param>
    /// <param name="magnitude">Интенсивность тряски.</param>
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        isShaking = true;
    }
}
