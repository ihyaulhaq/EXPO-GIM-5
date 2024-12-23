using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    // [SerializeField] private int maxHealth = 3;
    [SerializeField] private float extraGravityForce = 10f;
    [SerializeField] private Transform attackPoint;
    // [SerializeField] private LayerMask enemyLayers;
    // [SerializeField] private AudioClip swordAttack; // Efek suara pedang
    [SerializeField] private AudioSource audioSource; // Komponen AudioSource

    [Header("attack")]

    // [SerializeField] private float attackCooldown;
    // [SerializeField] private int attackDamage = 1;
    // [SerializeField] private float attackRange;
    // private float cooldownTimer = Mathf.Infinity;
    // private int currentHealth;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public bool dead = false;
    private Health health; // Reference to the Health script
    private UiManager uiManager;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // currentHealth = maxHealth;
        health = GetComponent<Health>(); // Get the Health component

        uiManager = FindAnyObjectByType<UiManager>();

        // Validasi audioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource belum diatur di Inspector atau pada GameObject ini.");
            }
        }
    }

    private void Update()
    {
        if (health == null || health.currentHealth <= 0)
        {
            if (!dead) // Only trigger once when the player dies
            {
                dead = true;
                anim.SetTrigger("die");
                uiManager.GameOver();
            }
            return; // Stop further updates when dead
        }

        // if (dead) return; 
        HandleMovement();
        // HandleAttack();
        UpdateAnimations();
    }

    private void HandleMovement()
    {


        // if (dead) return; // Prevent movement when dead
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Kontrol arah karakter
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(3, 3, 3);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-3, 3, 3);

        // Melompat
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();
    }

    // private void HandleAttack()
    // {
    //     // if (dead) return;

    //     if (Input.GetKeyDown(KeyCode.J))
    //     {
    //         Attack1();

    //         // Mainkan efek suara saat menyerang
    //         if (audioSource != null && swordAttack != null)
    //         {
    //             audioSource.PlayOneShot(swordAttack);
    //         }
    //     }
    // }

    private void UpdateAnimations()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void FixedUpdate()
    {
        if (!grounded)
        {
            body.AddForce(Vector2.down * extraGravityForce);
        }
    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        grounded = false;
    }


    // private void Attack1()
    // {
    //     anim.SetTrigger("attack");

    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         PatrolEnemy patrolEnemy = enemy.GetComponent<PatrolEnemy>();
    //         Health enemyHealth = enemy.GetComponent<Health>(); // Ambil komponen Health dari musuh
    //         if (enemyHealth != null)
    //         {
    //             enemyHealth.TakeDamage(attackDamage); // Berikan damage ke musuh
    //         }
    //         if (patrolEnemy != null)
    //         {
    //             patrolEnemy.TakeDamage(attackDamage); // Jika patrolEnemy memiliki logika tambahan
    //         }
    //     }

    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            Debug.Log("Grounded is true");
        }
        if (collision.gameObject.CompareTag("Finish")){

            uiManager.GameWin();
            Time.timeScale=0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            Debug.Log("Grounded is false");
        }
    }
}
