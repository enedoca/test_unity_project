using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shot
{
    public class CircleShot2 : MonoBehaviour
    {
        // 발사될 총알 프리팹
        public GameObject Bullet;

        // 이 스포너 오브젝트가 파괴될 때까지의 시간
        public float spawnerDestroyDelay = 5.0f;

        // 생성된 총알들을 저장할 리스트
        private List<BulletController> spawnedBullets = new List<BulletController>();

        // 현재 생성할 총알의 각도
        private int currentAngle = 0;

        // 발사가 완료되었는지 확인하는 플래그(flag)
        private bool hasFired = false;

        private void Start()
        {
            // 스포너는 일정 시간 후 스스로 파괴됩니다.
            Destroy(gameObject, spawnerDestroyDelay);
        }

        private void Update()
        {
            // 만약 발사가 이미 완료되었다면, Update 함수에서 아무 작업도 하지 않고 즉시 종료합니다.
            if (hasFired)
            {
                return;
            }

            // --- 1. 원형으로 총알을 생성하는 단계 ---
            if (currentAngle < 360)
            {
                // 총알 생성
                GameObject temp = Instantiate(Bullet, this.transform.position, Quaternion.identity);

                // 10초 후 총알이 스스로 파괴되도록 설정
                Destroy(temp, 10f);

                // 계산된 각도(currentAngle)로 총알을 회전시킵니다.
                temp.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

                // 생성된 총알에서 BulletController 컴포넌트를 가져와 리스트에 추가합니다.
                BulletController bulletController = temp.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    spawnedBullets.Add(bulletController);
                }

                // 다음 프레임에 생성될 총알의 각도를 증가시킵니다.
                currentAngle += 13;
            }
            // --- 2. 원 생성이 완료된 후 발사하는 단계 ---
            else
            {
                // 모든 총알이 생성되었으므로, 발사 플래그를 true로 설정합니다.
                // 이 else 블록은 단 한 번만 실행됩니다.
                hasFired = true;

                // 리스트에 저장해둔 모든 총알에게 발사 명령을 내립니다.
                foreach (BulletController bullet in spawnedBullets)
                {
                    if (bullet != null)
                    {
                        // BulletController의 Fire() 메소드를 호출하여 움직임을 시작시킵니다.
                        bullet.Fire();
                    }
                }
            }
        }
    }
}