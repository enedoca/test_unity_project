using UnityEngine;

public class Player : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        Debug.Log("Player destroyed"); // 로그 확인용
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.OnPlayerDestroyed();
        }
    }
}