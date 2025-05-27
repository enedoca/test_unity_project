using UnityEngine;
using System.Collections;


public class PowerItem : MonoBehaviour
{
    public float detectRange = 1.3f;         // Player�� ������ �Ÿ�
    public float moveSpeed = 8f;            // �̵� �ӵ�
    public float spinSpeed = 120f;          // ȸ�� �ӵ� (1�ʿ� �� �� ȸ������)
    public float duration = 1f;             // �̵� �� ȸ�� ���� �ð�
    public float slowDownFactor = 0.01f;     // �ö� �� õõ�� �ö󰡰� �ϴ� ���� ���


    private Transform player;               // Player�� Transform
    private bool isMoving = true;           // �̵� �� ȸ�� ����
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, 5f);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveAndSpin());
    }

    void Update()
    {
        if (isMoving && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // Player�� ���� �Ÿ� �̳��� ������ Player ������ �̵�
            if (distance <= detectRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator MoveAndSpin()
    {
        float timer = 0f;
        float totalRotation = Random.Range(360f, 720f);  // 360���� 720 ������ ������ ȸ�� ����

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // slowDownFactor�� ����Ͽ� y�ӵ��� õõ�� ���ҽ�Ŵ
            float verticalSpeed = Mathf.Lerp(moveSpeed, 0, timer / duration);
            verticalSpeed *= Mathf.Pow(slowDownFactor, timer);  // ���� ��� ����

            rb.linearVelocity = new Vector2(0, verticalSpeed);

            float rotationAmount = (totalRotation / duration) * Time.deltaTime;

            // ȸ��
            transform.Rotate(0, 0, rotationAmount);

            yield return null;
        }

        // 1�� ��, �������� ���ƿ�
        rb.linearVelocity = Vector2.zero;  // �̵� ����
        transform.rotation = Quaternion.Euler(0, 0, 0);  // ���� ���·� ȸ��

        // �� ���ĺ��ʹ� Player ���� �� �̵�
        isMoving = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player�� �浹 �� �Ҹ�
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
