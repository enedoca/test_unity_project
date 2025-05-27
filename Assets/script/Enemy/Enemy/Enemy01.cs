using UnityEngine;

public class Enemy01 : Enemy
{
    [SerializeField] private int health = 30;
    [SerializeField] private GameObject pointItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;

    protected override void OnTakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Enemy01 남은 체력: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void DropItems()
    {
        Vector2 origin = transform.position;

        // PointItem 5개
        for (int i = 0; i < 5; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 1f; // 반지름 1f 이내
            Instantiate(pointItemPrefab, origin + offset, Quaternion.identity);
        }

        // PowerItem 1개
        Vector2 powerOffset = Random.insideUnitCircle * 1f;
        Instantiate(powerItemPrefab, origin + powerOffset, Quaternion.identity);
    }

    private void Die()
    {
        DropItems();
        Destroy(gameObject);
    }
    //체력 30, 점수 5개, 파워 1개
}