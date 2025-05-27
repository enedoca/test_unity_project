using UnityEngine;
using System.Collections;


public class PowerItem : MonoBehaviour
{
    public float detectRange = 1.3f;         // Player를 감지할 거리
    public float moveSpeed = 8f;            // 이동 속도
    public float spinSpeed = 120f;          // 회전 속도 (1초에 몇 도 회전할지)
    public float duration = 1f;             // 이동 및 회전 지속 시간
    public float slowDownFactor = 0.01f;     // 올라갈 때 천천히 올라가게 하는 감속 계수


    private Transform player;               // Player의 Transform
    private bool isMoving = true;           // 이동 및 회전 여부
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

            // Player가 일정 거리 이내에 있으면 Player 쪽으로 이동
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
        float totalRotation = Random.Range(360f, 720f);  // 360에서 720 사이의 무작위 회전 각도

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // slowDownFactor를 사용하여 y속도를 천천히 감소시킴
            float verticalSpeed = Mathf.Lerp(moveSpeed, 0, timer / duration);
            verticalSpeed *= Mathf.Pow(slowDownFactor, timer);  // 감속 계수 적용

            rb.linearVelocity = new Vector2(0, verticalSpeed);

            float rotationAmount = (totalRotation / duration) * Time.deltaTime;

            // 회전
            transform.Rotate(0, 0, rotationAmount);

            yield return null;
        }

        // 1초 후, 수평으로 돌아옴
        rb.linearVelocity = Vector2.zero;  // 이동 멈춤
        transform.rotation = Quaternion.Euler(0, 0, 0);  // 수평 상태로 회전

        // 그 이후부터는 Player 감지 및 이동
        isMoving = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player와 충돌 시 소멸
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
