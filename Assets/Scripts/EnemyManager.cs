using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D playerRB;
    private float horizontal;
    private float vertical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 10f;
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TrackPlayer(2f));
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator TrackPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
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
        }
    }

}
