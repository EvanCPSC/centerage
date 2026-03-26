using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehoulder : MonoBehaviour
{
    public float speed;
    [SerializeField] private Rigidbody2D rb;
    private Rigidbody2D playerRB;
    private float horizontal;
    private float vertical;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = gameObject.GetComponent<EnemyManager>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        Invoke("TrackPlayer", 0.4f);

        // change anim
        if (horizontal == 0f && vertical == 0f)
        {
            anim.SetFloat("DirX", 1f);
        } else
        {
            anim.SetFloat("DirX", horizontal);
            anim.SetFloat("DirY", vertical);
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

}
