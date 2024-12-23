using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public float force;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float timer;
    private bool isExploding = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            // anim.SetTrigger("explode");
            // Destroy(gameObject);
            TriggerExplosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // other.gameObject.GetComponent<Health>().TakeDamage(1);
            Health playerHealth = other.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Damage the player
            }
            // anim.SetTrigger("explode");
            // Destroy(gameObject);
            TriggerExplosion();
        }
    }

    private void TriggerExplosion()
    {
        isExploding = true;
        anim.SetTrigger("explode");
        rb.velocity = Vector2.zero; // Stop movement
        boxCollider.enabled = false;
        // Disable further collisions
    }
    public void OnExplosionComplete()
    {
        Destroy(gameObject);
    }
}
