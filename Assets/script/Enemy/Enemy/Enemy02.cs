using UnityEngine;
using System.Collections; // 코루틴을 사용하기 위해 필요

public class Enemy02 : Enemy
{
    [SerializeField] private int health = 30;
    [SerializeField] private GameObject pointItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;

    // --- 추가된 부분 시작 ---
    [Header("Shooting")]
    [SerializeField] private GameObject shotPrefab; // 발사할 오브젝트의 프리팹
    [SerializeField] private float shootInterval = 3f;  // 발사 간격 (3초)
    // --- 추가된 부분 끝 ---

    // Start 함수는 오브젝트가 생성될 때 한 번 호출됩니다.
    // 여기에 코루틴을 시작하는 코드를 넣습니다.
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

        // PointItem 5개
        for (int i = 0; i < 5; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 1f;
            Instantiate(pointItemPrefab, origin + offset, Quaternion.identity);
        }

        // PowerItem 1개
        Vector2 powerOffset = Random.insideUnitCircle * 1f;
        Instantiate(powerItemPrefab, origin + powerOffset, Quaternion.identity);
    }

    private void Die()
    {
        // 오브젝트가 파괴되면 실행 중인 모든 코루틴이 자동으로 멈춥니다.
        DropItems();
        Destroy(gameObject);
    }

    // --- 추가된 부분 시작 ---

    /// <summary>
    /// 발사체(Shot)를 생성하는 실제 로직을 담은 함수
    /// </summary>
    private void Shoot()
    {
        // shotPrefab이 할당되었는지 확인 (실수 방지)
        if (shotPrefab != null)
        {
            // 이 적( Enemy01)의 현재 위치에 shotPrefab을 생성합니다.
            Instantiate(shotPrefab, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// 일정 시간(shootInterval)마다 발사를 반복하는 코루틴
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        // 이 루틴은 적이 살아있는 동안 계속 반복됩니다.
        while (true)
        {
            // shootInterval(5초)만큼 기다립니다.
            yield return new WaitForSeconds(shootInterval);

            // 5초가 지난 후, Shoot 함수를 호출하여 발사체를 생성합니다.
            Shoot();
        }
    }
    // --- 추가된 부분 끝 ---
}