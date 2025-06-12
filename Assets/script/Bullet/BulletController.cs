using UnityEngine;

    public class BulletController : MonoBehaviour {

        public float Speed = 5f;
        private bool isMoving = false;
       
        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void Update()
        {
            if (isMoving)
            {
                transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
            }
            transform.Translate(Vector2.right * (Speed * Time.deltaTime), Space.Self);
        }

        public void Fire()
        {
            isMoving = true;
        }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}