using UnityEngine;
using System.Collections;
public class RuntimeExampleTest
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float damage;
    [SerializeField] private float cooldownTime;
    [SerializeField] private int bulletCount;
    [SerializeField] private float bulletSpeed;
    public void Shoot()
    {
        float angleStep = 30f / (bulletCount - 1);
        float angle = -15f;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.Euler(0, 0, angle));
            if(bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                rb.AddForce(direction * bulletSpeed);
            }
            angle += angleStep;
        }
    }

    public void Spread()
    {
        float angleStep = 30f / (bulletCount - 1);
        float angle = -15f;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.Euler(0, 0, angle));
            if(bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                rb.AddForce(direction * bulletSpeed);
            }
            angle += angleStep;
        }
    }
}