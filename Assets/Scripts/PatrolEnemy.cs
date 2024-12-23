using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Movement Settings")]
    [SerializeField] private bool facingLeft = true;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float distance = 1f;
    [SerializeField] private LayerMask LayerMask;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float bulletSpeed = 5f;

    private float shootTimer;
    private Animator anim;
    private bool isDead = false;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;

    void Start()
    {
        shootTimer = shootInterval;
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        if (facingLeft)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }

        // Ubah arah berdasarkan posisi X
        if (facingLeft && transform.position.x <= 8f)
        {
            Flip(false);
        }
        else if (!facingLeft && transform.position.x >= 10.38f)
        {
            Flip(true);
        }
    }

    private void Flip(bool toLeft)
    {
        facingLeft = toLeft;
        transform.rotation = Quaternion.Euler(0, toLeft ? 0 : 180, 0);
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                float direction = facingLeft ? -1f : 1f;
                bulletRb.velocity = new Vector2(direction * bulletSpeed, 0);
            }
            
            // Destroy bullet after 5 seconds to prevent memory leaks
            Destroy(bullet, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}/{maxHealth}");

        // Play hit animation if you have one
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        
        isDead = true;
        Debug.Log("Enemy Died!");

        // Disable components
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (rb != null) rb.simulated = false;
        
        // Stop movement and shooting
        moveSpeed = 0;
        shootTimer = Mathf.Infinity;

        // Play death animation
        anim.SetTrigger("TentaraDead");

        // Destroy the enemy after animation finishes
        float deathAnimationLength = 1f; // Sesuaikan dengan durasi animasi kematian
        Destroy(gameObject, deathAnimationLength);
    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint == null) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
    }
}