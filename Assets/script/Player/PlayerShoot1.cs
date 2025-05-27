using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.1f;
    public float offsetX = 0.1f;

    private float lastFireTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time >= lastFireTime + fireRate)
        {
            lastFireTime = Time.time;
            ShootFromSides();
        }
    }

    void ShootFromSides()
    {
        Vector3 leftPos = transform.position + Vector3.left * offsetX;
        Vector3 rightPos = transform.position + Vector3.right * offsetX;

        Instantiate(bulletPrefab, leftPos, Quaternion.identity);
        Instantiate(bulletPrefab, rightPos, Quaternion.identity);
    }
}
