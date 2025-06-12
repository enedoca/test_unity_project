using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shot
{
    public class CircleShot2 : MonoBehaviour
    {
        // �߻�� �Ѿ� ������
        public GameObject Bullet;

        // �� ������ ������Ʈ�� �ı��� �������� �ð�
        public float spawnerDestroyDelay = 5.0f;

        // ������ �Ѿ˵��� ������ ����Ʈ
        private List<BulletController> spawnedBullets = new List<BulletController>();

        // ���� ������ �Ѿ��� ����
        private int currentAngle = 0;

        // �߻簡 �Ϸ�Ǿ����� Ȯ���ϴ� �÷���(flag)
        private bool hasFired = false;

        private void Start()
        {
            // �����ʴ� ���� �ð� �� ������ �ı��˴ϴ�.
            Destroy(gameObject, spawnerDestroyDelay);
        }

        private void Update()
        {
            // ���� �߻簡 �̹� �Ϸ�Ǿ��ٸ�, Update �Լ����� �ƹ� �۾��� ���� �ʰ� ��� �����մϴ�.
            if (hasFired)
            {
                return;
            }

            // --- 1. �������� �Ѿ��� �����ϴ� �ܰ� ---
            if (currentAngle < 360)
            {
                // �Ѿ� ����
                GameObject temp = Instantiate(Bullet, this.transform.position, Quaternion.identity);

                // 10�� �� �Ѿ��� ������ �ı��ǵ��� ����
                Destroy(temp, 10f);

                // ���� ����(currentAngle)�� �Ѿ��� ȸ����ŵ�ϴ�.
                temp.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

                // ������ �Ѿ˿��� BulletController ������Ʈ�� ������ ����Ʈ�� �߰��մϴ�.
                BulletController bulletController = temp.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    spawnedBullets.Add(bulletController);
                }

                // ���� �����ӿ� ������ �Ѿ��� ������ ������ŵ�ϴ�.
                currentAngle += 13;
            }
            // --- 2. �� ������ �Ϸ�� �� �߻��ϴ� �ܰ� ---
            else
            {
                // ��� �Ѿ��� �����Ǿ����Ƿ�, �߻� �÷��׸� true�� �����մϴ�.
                // �� else ����� �� �� ���� ����˴ϴ�.
                hasFired = true;

                // ����Ʈ�� �����ص� ��� �Ѿ˿��� �߻� ����� �����ϴ�.
                foreach (BulletController bullet in spawnedBullets)
                {
                    if (bullet != null)
                    {
                        // BulletController�� Fire() �޼ҵ带 ȣ���Ͽ� �������� ���۽�ŵ�ϴ�.
                        bullet.Fire();
                    }
                }
            }
        }
    }
}