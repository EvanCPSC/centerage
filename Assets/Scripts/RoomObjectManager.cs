using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomObjectManager : MonoBehaviour
{
    GameManager gameManager;
    public bool spawned;
    private int rand;
    void Start()
    {
        spawned = false;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void SpawnObject()
    {
        rand = Random.Range(0, 10);
        if (rand < 2) // spawn item
        {
            rand = Random.Range(0, gameManager.items.Length);
            Instantiate(gameManager.items[rand], new Vector3(transform.position.x, transform.position.y, -3f), Quaternion.identity);
        } else if (rand < 8) // spawn enemy
        {
            rand = Random.Range(0, gameManager.enemies.Length);
            Instantiate(gameManager.enemies[rand], new Vector3(transform.position.x, transform.position.y, -3f), Quaternion.identity);
        } else // spawn nothing
        {
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !spawned)
        {
            SpawnObject();
            spawned = true;
        }
    }
}
