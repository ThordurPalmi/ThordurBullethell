using System.Collections;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 500f;
    public float bulletLifetime = 2f;
    public float fireRate = 2f;
    private int bulletDamage = 10; // Define bulletDamage within the BossBehavior class
    private int currentHealth;
    private Transform playerTransform;
    private float nextFire = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        // Move the boss
        // Perform attacks
        // Check if the boss has been defeated
        if (currentHealth <= 0)
        {
            // Trigger the next stage or end the game
        }
        else
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Fire();
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (playerTransform.position - bullet.transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
