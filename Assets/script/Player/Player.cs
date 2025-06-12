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
        Debug.Log("Player destroyed"); // �α� Ȯ�ο�
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.OnPlayerDestroyed();
        }
    }
}