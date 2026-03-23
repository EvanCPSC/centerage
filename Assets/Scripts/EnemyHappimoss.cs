using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHappimoss : MonoBehaviour
{
    private int randX, randY;
    [SerializeField] private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randX = Random.Range(-2, 3);
        randY = Random.Range(-1, 2);
        transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, -3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}