using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PickaxeProjectile : MonoBehaviour
{
    
    public char dir = ' ';
    private Rigidbody2D playerRB;
    private Vector2 playerDir;
    // AI Mode. "unity throw boomerang through player 2d", Google, 24 Feb. 2026, https://share.google/aimode/flPMVYw6VFeF3V5jw.
    // ------------
    public float speed;
    public float angle;
    public float radius;
    public float throwDistance; // Max distance before returning
    public Vector2 startPosition;
    private Transform playerTransform; // Reference to the player's transform
    private GameObject playerObj;
    public float pearlYRange;

    void Start()
    {
        if (PlayerStats.moonstone)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
        startPosition = transform.position;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObj.transform;
        playerRB = playerObj.GetComponent<Rigidbody2D>();
        playerDir = playerRB.linearVelocity;
        throwDistance = PlayerStats.pickaxeRange / 1.75f;
        speed = PlayerStats.pickaxeForce;
        PlayerStats.pickaxeReturning = false;
        PlayerStats.pickaxeRetrieved = false;
        angle = 0;
        radius = 0;
        if (PlayerStats.pearl)
        {
            pearlYRange = PlayerStats.pearlRange;
        } else
        {
            pearlYRange = 0f;
        }
        if (PlayerStats.pearlAlt)
        {
            pearlYRange *= -1f;
        }
        // ----
    }

    void Update()
    {
        if (!PlayerStats.pickaxeReturning)
        {
            // ---- chatgpt
            if (PlayerStats.moonstoneUsed)
            {
                angle += 1.2f * speed * Time.deltaTime;

                // Gradually increase radius until it reaches throwDistance
                radius += speed * Time.deltaTime / (Mathf.PI);

                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                transform.position = (Vector2)playerTransform.position + offset;
            // ----
            } else {
                // Move away from the start point
                // ---- did the direction switch myself
                switch(dir)
                {
                    case 'L':
                        if (playerDir.x > 0f)
                        {
                            throwDistance -= 0.6f;
                        } else if (playerDir.x < 0f)
                        {
                            throwDistance += 0.6f;
                        }
                        transform.Translate(new Vector2(-1f * speed * Time.deltaTime, pearlYRange * speed * Time.deltaTime));
                        break;
                    case 'R':
                        if (playerDir.x > 0f)
                        {
                            throwDistance += 0.6f;
                        } else if (playerDir.x < 0f)
                        {
                            throwDistance -= 0.6f;
                        }
                        transform.Translate(new Vector2(1f * speed * Time.deltaTime, pearlYRange * speed * Time.deltaTime));
                        break;
                    case 'U':
                        if (playerDir.y > 0f)
                        {
                            throwDistance += 0.6f;
                        } else if (playerDir.y < 0f)
                        {
                            throwDistance -= 0.6f;
                        }
                        transform.Translate(new Vector2(pearlYRange * speed * Time.deltaTime, 1f * speed * Time.deltaTime));
                        break;
                    case 'D':
                        if (playerDir.y > 0f)
                        {
                            throwDistance -= 0.6f;
                        } else if (playerDir.y < 0f)
                        {
                            throwDistance += 0.6f;
                        }
                        transform.Translate(new Vector2(pearlYRange * speed * Time.deltaTime, -1f * speed * Time.deltaTime));
                        break;
                    default:
                        break;
                }
                // ----

                // Check if max distance is reached
                if (Vector2.Distance(startPosition, transform.position) >= throwDistance || radius >= throwDistance - 1f)
                {
                    PlayerStats.pickaxeReturning = true;
                    radius = throwDistance;
                }
                
                // ----
                throwDistance = PlayerStats.pickaxeRange / 1.75f;
                // ----
            }
        }
        else
        {
            // Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>(), true);
            // Move towards the player's current position
            if (PlayerStats.moonstoneUsed)
            {
                angle -= 1.2f * speed * Time.deltaTime;

                radius -= speed * Time.deltaTime / (Mathf.PI);

                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                transform.position = (Vector2)playerTransform.position + offset;

            } else {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }

            // Check if it's back to the player
            if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
            {
                playerObj.GetComponent<PlayerMovement>().throwDelay = 1f;
                Destroy(gameObject);
                PlayerStats.pickaxeRetrieved = true;
                PlayerStats.moonstoneUsed = false;
                PlayerStats.sunstoneUsed = false;
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