using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHappimoss : MonoBehaviour
{
    public float health;
    private bool isHit;
    private float hitFrames;
    private SpriteRenderer spriteRenderer;
    public AudioManager audioManager;
    private int randX, randY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = AudioManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHit = false;
        hitFrames = 0;
        randX = Random.Range(-2, 3);
        randY = Random.Range(-1, 2);
        transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, -3f);
    }

    // Update is called once per frame
    void Update()
    {
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