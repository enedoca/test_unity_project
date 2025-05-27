using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        // 위 방향으로 속도 설정
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * speed;

        // 일정 시간 후 자동 파괴
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 이제 Enemy01, Enemy02가 알아서 처리
            }

            Destroy(gameObject); // 탄환 제거
        }
    }
}