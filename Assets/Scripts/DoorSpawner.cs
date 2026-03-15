using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSpawner : MonoBehaviour
{
    public List<int> doorDirections = new List<int>();
    // 1 -> right door
    // 2 -> left door
    // 3 -> bottom door
    // 4 -> top door
    public GameObject[] doors;

    void Start()
    {
        Invoke("StartSpawning", 2f);
    }

    void StartSpawning()
    {
        foreach (int dir in doorDirections)
        {
            SpawnDoor(dir);
        }
    }

    void SpawnDoor(int dir)
    {
        switch(dir)
        {
            case 1: // right door
                Instantiate(doors[0], new Vector3(transform.position.x + 8f, transform.position.y, 1f), Quaternion.identity);
                break;
            case 2: // left door
                Instantiate(doors[1], new Vector3(transform.position.x - 8f, transform.position.y, 1f), Quaternion.identity);
                break;
            case 3: // bottom door
                Instantiate(doors[2], new Vector3(transform.position.x, transform.position.y - 4.175f, 1f), Quaternion.identity);
                break;
            case 4: // top door
                Instantiate(doors[3], new Vector3(transform.position.x, transform.position.y + 4.175f, 1f), Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
