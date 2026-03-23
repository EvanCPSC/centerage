using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public float health;
    public float speed;
    private bool isHit;
    private float hitFrames;
    private SpriteRenderer spriteRenderer;
    public AudioManager audioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = AudioManager.Instance;
        isHit = false;
        hitFrames = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // show hit
        if (isHit)
        {
            spriteRenderer.color = new Color(1f, hitFrames, hitFrames, 1f);
            hitFrames += 0.1f;
            if (hitFrames > 1f)
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

    public void HitEnemy(float damage)
    {
        health -= damage;
        isHit = true;
        audioManager.PlaySFX(audioManager.sfxEnemyDie);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pickaxe"))
        {
            HitEnemy(PlayerStats.pickaxeDamage);
        }
    }

}
