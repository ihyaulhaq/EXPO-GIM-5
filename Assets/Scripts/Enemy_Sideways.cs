using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    
    [Header("Sprite Settings")]
    [SerializeField] private bool startFacingLeft = true; // Menentukan arah hadap awal

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        
        // Set rotasi awal berdasarkan arah hadap awal
        transform.rotation = Quaternion.Euler(0, startFacingLeft ? 180 : 0, 0);
        movingLeft = startFacingLeft;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
                FlipSprite();
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
                FlipSprite();
            }
        }
    }

    private void FlipSprite()
    {
        // Rotasi sprite 180 derajat pada sumbu Y
        transform.rotation = Quaternion.Euler(0, movingLeft ? -180 : 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}