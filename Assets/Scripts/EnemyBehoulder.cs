using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehoulder : MonoBehaviour
{
    public float health;
    public float speed;
    private bool isHit;
    private float hitFrames;
    [SerializeField] private Rigidbody2D rb;
    private Rigidbody2D playerRB;
    private float horizontal;
    private float vertical;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public AudioManager audioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = AudioManager.Instance;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isHit = false;
        hitFrames = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        Invoke("TrackPlayer", 0.4f);

        // change anim
        if (horizontal == 0 && vertical == 0)
        {
            anim.SetFloat("DirX", 1);
        } else
        {
            anim.SetFloat("DirX", horizontal);
            anim.SetFloat("DirY", vertical);
        }

        // show hit
        if (isHit)
        {
            spriteRenderer.color = new Color(1f, hitFrames, hitFrames, 1f);
            hitFrames += 0.1f;
            if (hitFrames >= 1f)
            {
                hitFrames = 0f;
                isHit = false;
            }
        }

        // die
        if (health <= 0)
        {
            Destroy(gameObject);
        }

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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickaxe"))
        {
            health -= PlayerStats.pickaxeDamage;
            isHit = true;
            audioManager.PlaySFX(audioManager.sfxEnemyDie);
        }
    }

}
