using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
        float horizontalInput = Input.GetAxis("Horizontal");





    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();

        // anim = GetComponent<Animator>();
        // currentHealth = maxHealth;

        // // Validasi audioSource
        // if (audioSource == null)
        // {
        //     audioSource = GetComponent<AudioSource>();
        //     if (audioSource == null)
        //     {
        //         Debug.LogError("AudioSource belum diatur di Inspector atau pada GameObject ini.");
        //     }
        // }
    }
    private void Update()
    {

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Kontrol arah karakter
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();

        //anim parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }

}
