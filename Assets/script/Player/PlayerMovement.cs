using UnityEngine;

namespace other
{
    public class Player : MonoBehaviour
    {
        public float Speed = 8f;
        public float Speed_Slow = 4f;
        public Vector2 minBound = new Vector2(-4f, -4.5f); // 필드 왼쪽 아래
        public Vector2 maxBound = new Vector2(4f, 4.5f);   // 필드 오른쪽 위

        private Vector2 halfSize; // 플레이어 절반 크기 (localScale 기준)

        private void Start()
        {
            // SpriteRenderer의 크기와 localScale을 기반으로 반지름 계산
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Vector2 spriteSize = sr.bounds.size;
                halfSize = spriteSize / 2f;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer가 없습니다. 기본값 사용");
                halfSize = new Vector2(0.1f, 0.1f); // 임시
            }
        }

        private void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // Shift 키를 누르면 느린 속도 적용
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Speed_Slow : Speed;

            Vector3 nextPos = transform.position + (Vector3)(input * currentSpeed * Time.deltaTime);

            // 반지름을 고려해서 Clamp
            float clampedX = Mathf.Clamp(nextPos.x, minBound.x + halfSize.x, maxBound.x - halfSize.x);
            float clampedY = Mathf.Clamp(nextPos.y, minBound.y + halfSize.y, maxBound.y - halfSize.y);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
