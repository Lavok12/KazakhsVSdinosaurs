using UnityEngine;

public class JumpTriggerController : MonoBehaviour
{
    [Header("Jump Trigger Settings")]
    public Collider2D jumpTrigger; // Ссылка на триггер
    public bool isTouchingBlock; // Состояние обычного блока
    public bool isTouchingSlipperyBlock; // Состояние скользкого блока

    private ContactFilter2D contactFilter; // Фильтр для поиска коллизий

    private void Start()
    {
        // Настраиваем фильтр для обнаружения объектов с определенными тегами
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useTriggers = true;
    }

    private void Update()
    {
        UpdateCollisionState();
    }

    private void UpdateCollisionState()
    {
        Collider2D[] results = new Collider2D[10]; // Массив для хранения результатов
        int numColliders = jumpTrigger.OverlapCollider(contactFilter, results);

        // Сброс состояния перед проверкой
        isTouchingBlock = false;
        isTouchingSlipperyBlock = false;

        for (int i = 0; i < numColliders; i++)
        {
            if (results[i].CompareTag("Block"))
            {
                isTouchingBlock = true;
            }
            else if (results[i].CompareTag("SlipperyBlock"))
            {
                isTouchingSlipperyBlock = true;
            }
        }
    }
}
