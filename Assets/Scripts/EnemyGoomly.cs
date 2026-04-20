using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGoomly : MonoBehaviour
{
    public float speed;
    private float shootDelay;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject lavaballPrefab;
    public GameObject lavaball = null;
    private Rigidbody2D playerRB;
    private float horizontal;
    private float vertical;
    private float lastHorizontal;
    private float lastVertical;
    private Animator anim;
    public LayerMask playerLayer;
    private bool isShooting = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = gameObject.GetComponent<EnemyManager>().speed;
        shootDelay = 0f;
        lastHorizontal = 0f;
        lastVertical = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        if (!isShooting)
        {
            Invoke("TrackPlayer", 0.4f);
        } else
        {
            horizontal = 0f;
            vertical = 0f;
        }

        if (horizontal != 0 || vertical != 0)
            {
                anim.SetFloat("DirX", horizontal);
                anim.SetFloat("DirY", vertical);
                lastHorizontal = horizontal;
                lastVertical = vertical;
            } else
            {
                anim.SetFloat("LastDirX", lastHorizontal);
                anim.SetFloat("LastDirY", lastVertical);
            }

        // shoot if player nearby
        if (shootDelay <= 0f) {
            if (Physics2D.OverlapCircle(transform.position, 5f, playerLayer))
            {
                anim.SetBool("isShooting", true);
                isShooting = true;
                shootDelay = Random.Range(1.5f, 2f);
                Invoke("ShootLavaball", 0.5f);
            }
        } else
        {
            shootDelay -= 0.01f;
        }

    }

    private void ShootLavaball()
    {
        anim.SetBool("isShooting", false);
        lavaball = Instantiate(lavaballPrefab, transform.position, Quaternion.identity);
        ProjectileLavaball lavaballScript = lavaball.GetComponent<ProjectileLavaball>();
        if (lavaballScript != null)
        {
            lavaballScript.dir = GameObject.FindGameObjectWithTag("Player").transform.position;
            lavaballScript.startPos = gameObject.transform.position;
        }
        isShooting = false;
    }

    private void TrackPlayer()
    {
        if (playerRB.position.x < rb.position.x)
        {
            horizontal = -1f;
        }
        else
        {
            horizontal = 1f;
        }
        if (playerRB.position.y < rb.position.y)
        {
            vertical = -1f;
        }
        else
        {
            vertical = 1f;
        }
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

}
