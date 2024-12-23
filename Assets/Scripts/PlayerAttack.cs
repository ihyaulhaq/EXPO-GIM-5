using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip swordAttack;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Enemy Layer")]

    [SerializeField] private LayerMask enemyLayer;
    private float cooldownTimer = Mathf.Infinity;

    // References
    private Animator anim;
    private Health enemyHealth;
    private PatrolEnemy patrolEnemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Check if "J" is pressed for attack
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer >= attackCooldown)
        {

            if (audioSource != null && swordAttack != null)
            {
                audioSource.PlayOneShot(swordAttack);
            }
            anim.SetTrigger("attack");
            cooldownTimer = 0;
            enemyInSight(); // Attack the enemy in front
        }
    }


    private bool enemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.zero, 0, enemyLayer);

        if (hit.collider != null)
        {
            patrolEnemy = hit.transform.GetComponent<PatrolEnemy>();
            enemyHealth = hit.transform.GetComponent<Health>(); // Ambil komponen Health dari musuh
        }
        return hit.collider !=null;
    }

    private void DamageEnemy()
    {
        if (enemyInSight())
        {
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Berikan damage ke musuh
            }
            if (patrolEnemy != null)
            {
                patrolEnemy.TakeDamage(damage); // Jika patrolEnemy memiliki logika tambahan
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        // Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x, boxCollider.bounds.size);
    }
}