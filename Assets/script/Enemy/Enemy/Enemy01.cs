using UnityEngine;

public class Enemy01 : Enemy
{
    [SerializeField] private int health = 30;
    [SerializeField] private GameObject pointItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;

    protected override void OnTakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Enemy01 ���� ü��: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void DropItems()
    {
        Vector2 origin = transform.position;

        // PointItem 5��
        for (int i = 0; i < 5; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 1f; // ������ 1f �̳�
            Instantiate(pointItemPrefab, origin + offset, Quaternion.identity);
        }

        // PowerItem 1��
        Vector2 powerOffset = Random.insideUnitCircle * 1f;
        Instantiate(powerItemPrefab, origin + powerOffset, Quaternion.identity);
    }

    private void Die()
    {
        DropItems();
        Destroy(gameObject);
    }
    //ü�� 30, ���� 5��, �Ŀ� 1��
}