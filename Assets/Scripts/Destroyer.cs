using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(other.gameObject);
        }
    }
}
