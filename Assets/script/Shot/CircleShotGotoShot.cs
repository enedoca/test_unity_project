using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shot
{
    public class CircleShotGotoShot : MonoBehaviour
    {
        //총알을 생성후 Target에게 날아갈 변수
        private Transform Target;

        //발사될 총알 오브젝트
        public GameObject Bullet;

        // 오브젝트가 파괴될 때까지의 시간 (초 단위)
        public float delay = 5.0f;

        private void Awake()
        {
            // "Player"라는 태그를 가진 게임 오브젝트를 찾습니다.
            GameObject playerObject = GameObject.FindWithTag("Player");

            // 만약 플레이어 오브젝트를 찾았다면
            if (playerObject != null)
            {
                // 해당 오브젝트의 Transform 컴포넌트를 target 변수에 할당합니다.
                Target = playerObject.transform;
            }
        }
        private void Start()
        {
            Shot();
            Destroy(gameObject, delay);
        }

        private void Shot()
        {
            //Target방향으로 발사될 오브젝트 수록
            List<Transform> bullets = new List<Transform>();
            
            for (int i = 0; i < 360; i += 13)
            {
                GameObject temp = Instantiate(Bullet);

                Destroy(temp, 10f);

                temp.transform.position = this.transform.position;

                bullets.Add(temp.transform);

                temp.transform.rotation = Quaternion.Euler(0, 0, i);
            }

            //총알을 Target 방향으로 이동시킨다.
            StartCoroutine(BulletToTarget(bullets));
        }

        private IEnumerator BulletToTarget(IList<Transform> objects)
        {
            //0.5초 후에 시작
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < objects.Count; i++)
            {
                //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
                Vector3 targetDirection = Target.transform.position - objects[i].position;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

                //Target 방향으로 이동
                objects[i].rotation = Quaternion.Euler(0, 0, angle);
            }

            //데이터 해제
            objects.Clear();
        }
    }
}