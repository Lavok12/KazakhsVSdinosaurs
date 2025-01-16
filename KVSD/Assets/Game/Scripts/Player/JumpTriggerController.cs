using UnityEngine;

public class JumpTriggerController : MonoBehaviour
{
    [Header("Jump Trigger Settings")]
    public Collider2D jumpTrigger; // ������ �� �������
    public bool isTouchingBlock; // ��������� �������� �����
    public bool isTouchingSlipperyBlock; // ��������� ���������� �����

    private ContactFilter2D contactFilter; // ������ ��� ������ ��������

    private void Start()
    {
        // ����������� ������ ��� ����������� �������� � ������������� ������
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useTriggers = true;
    }

    private void Update()
    {
        UpdateCollisionState();
    }

    private void UpdateCollisionState()
    {
        Collider2D[] results = new Collider2D[10]; // ������ ��� �������� �����������
        int numColliders = jumpTrigger.OverlapCollider(contactFilter, results);

        // ����� ��������� ����� ���������
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
