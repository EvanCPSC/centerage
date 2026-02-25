using UnityEngine;
using System.Collections;


public class PickaxeProjectile : MonoBehaviour
{
    
    public char dir = ' ';
    private Rigidbody2D playerRB;
    private Vector2 playerDir;
    public float damage;
    // AI Mode. "unity throw boomerang through player 2d", Google, 24 Feb. 2026, https://share.google/aimode/flPMVYw6VFeF3V5jw.
    // ------------
    public float speed;
    public float throwDistance; // Max distance before returning
    private Vector2 startPosition;
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        startPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // ----
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerDir = playerRB.linearVelocity;
        throwDistance = PlayerStats.pickaxeRange;
        damage = PlayerStats.pickaxeDamage;
        speed = PlayerStats.pickaxeSpeed;
        PlayerStats.pickaxeReturning = false;
        // ----
    }

    void Update()
    {
        if (!PlayerStats.pickaxeReturning)
        {
            // Move away from the start point
            // ---- did the direction switch myself
            switch(dir)
            {
                case 'L':
                    if (playerDir.x > 0f)
                    {
                        throwDistance -= 1f;
                    } else if (playerDir.x < 0f)
                    {
                        throwDistance += 1f;
                    }
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    break;
                case 'R':
                    if (playerDir.x > 0f)
                    {
                        throwDistance += 1f;
                    } else if (playerDir.x < 0f)
                    {
                        throwDistance -= 1f;
                    }
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    break;
                case 'U':
                    if (playerDir.y > 0f)
                    {
                        throwDistance += 1f;
                    } else if (playerDir.y < 0f)
                    {
                        throwDistance -= 1f;
                    }
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                    break;
                case 'D':
                    if (playerDir.y > 0f)
                    {
                        throwDistance -= 1f;
                    } else if (playerDir.y < 0f)
                    {
                        throwDistance += 1f;
                    }
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                    break;
                default:
                    break;
            }
            // ----

            // Check if max distance is reached
            if (Vector2.Distance(startPosition, transform.position) >= throwDistance)
            {
                PlayerStats.pickaxeReturning = true;
            }
            
            // ----
            throwDistance = PlayerStats.pickaxeRange;
            // ----
        }
        else
        {
            // Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>(), true);
            // Move towards the player's current position
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

            // Check if it's back to the player
            if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
            {
                Destroy(gameObject);
            }
            
            // Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>(), false);
        }
        
    }
    // ------------

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            PlayerStats.pickaxeReturning = true;
        }
    }
}