using UnityEngine;
using System.Collections;

public class Enemy01 : Enemy
{
    [SerializeField] private int health = 30;
    [SerializeField] private GameObject pointItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;

    [Header("Shooting")]
    [SerializeField] private GameObject shotPrefab; // 발사할 오브젝트의 프리팹
    [SerializeField] private float shootInterval = 2f;  // 발사 간격 (2초)

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

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

        for (int i = 0; i < 5; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 1f;
            Instantiate(pointItemPrefab, origin + offset, Quaternion.identity);
        }

        Vector2 powerOffset = Random.insideUnitCircle * 1f;
        Instantiate(powerItemPrefab, origin + powerOffset, Quaternion.identity);
    }

    private void Die()
    {
        DropItems();
        Destroy(gameObject);
    }


    private void Shoot()
    {
        if (shotPrefab != null)
        {
            // 이 적( Enemy01)의 현재 위치에 shotPrefab을 생성합니다.
            Instantiate(shotPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            // shootInterval만큼 기다립니다.
            yield return new WaitForSeconds(shootInterval);
            Shoot();
        }
    }
}