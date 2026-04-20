using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProjectileLavaball : MonoBehaviour
{
    
    [SerializeField] public bool fromPlayer;
    public float speed;
    public Vector2 dir;
    public Vector2 startPos;
    private Vector2 endpos;

    void Start()
    {
        endpos = new Vector2(dir.x - startPos.x, dir.y - startPos.y);
        speed = 2f;
    }

    void Update()
    {
        transform.Translate(new Vector2(endpos.x * speed * Time.deltaTime, endpos.y * speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (fromPlayer)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        } else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}