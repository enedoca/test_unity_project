using UnityEngine;
using System.Collections; // �ڷ�ƾ�� ����ϱ� ���� �ʿ�

public class Enemy02 : Enemy
{
    [SerializeField] private int health = 30;
    [SerializeField] private GameObject pointItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;

    // --- �߰��� �κ� ���� ---
    [Header("Shooting")]
    [SerializeField] private GameObject shotPrefab; // �߻��� ������Ʈ�� ������
    [SerializeField] private float shootInterval = 3f;  // �߻� ���� (3��)
    // --- �߰��� �κ� �� ---

    // Start �Լ��� ������Ʈ�� ������ �� �� �� ȣ��˴ϴ�.
    // ���⿡ �ڷ�ƾ�� �����ϴ� �ڵ带 �ֽ��ϴ�.
    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

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
            Vector2 offset = Random.insideUnitCircle * 1f;
            Instantiate(pointItemPrefab, origin + offset, Quaternion.identity);
        }

        // PowerItem 1��
        Vector2 powerOffset = Random.insideUnitCircle * 1f;
        Instantiate(powerItemPrefab, origin + powerOffset, Quaternion.identity);
    }

    private void Die()
    {
        // ������Ʈ�� �ı��Ǹ� ���� ���� ��� �ڷ�ƾ�� �ڵ����� ����ϴ�.
        DropItems();
        Destroy(gameObject);
    }

    // --- �߰��� �κ� ���� ---

    /// <summary>
    /// �߻�ü(Shot)�� �����ϴ� ���� ������ ���� �Լ�
    /// </summary>
    private void Shoot()
    {
        // shotPrefab�� �Ҵ�Ǿ����� Ȯ�� (�Ǽ� ����)
        if (shotPrefab != null)
        {
            // �� ��( Enemy01)�� ���� ��ġ�� shotPrefab�� �����մϴ�.
            Instantiate(shotPrefab, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// ���� �ð�(shootInterval)���� �߻縦 �ݺ��ϴ� �ڷ�ƾ
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        // �� ��ƾ�� ���� ����ִ� ���� ��� �ݺ��˴ϴ�.
        while (true)
        {
            // shootInterval(5��)��ŭ ��ٸ��ϴ�.
            yield return new WaitForSeconds(shootInterval);

            // 5�ʰ� ���� ��, Shoot �Լ��� ȣ���Ͽ� �߻�ü�� �����մϴ�.
            Shoot();
        }
    }
    // --- �߰��� �κ� �� ---
}