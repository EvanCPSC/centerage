using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 -> need right door
    // 2 -> need left door
    // 3 -> need bottom door
    // 4 -> need top door


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime); // delete inactive spawner for memory
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            switch (openingDirection)
            {
                case 1:
                    // need to spawn room with right door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 2:
                    // need to spawn room with left door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 3:
                    // need to spawn room with bottom door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 4:
                    // need to spawn room with top door
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            spawned = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) {
            if (other.CompareTag("SpawnPoint"))
            {
            
                if (other.GetComponent<RoomSpawner>().spawned == false && !spawned)
                {
                    // spawn some kinda other room to close it off
                    GameObject temproom = templates.closedRoom;
                    // bool madeRoom = false;
                    // int otherDir = other.gameObject.GetComponent<RoomSpawner>().openingDirection;
                    // if ((openingDirection == 1 && otherDir == 3) || (openingDirection == 3 && otherDir == 1))
                    // {
                    //     // need right and bottom door
                    //     temproom = templates.getRoom("RB");
                    //     madeRoom = true;
                    // } else if ((openingDirection == 1 && otherDir == 4) || (openingDirection == 4 && otherDir == 1))
                    // {
                    //     // need top and right door
                    //     temproom = templates.getRoom("TR");
                    //     madeRoom = true;
                    // } else if ((openingDirection == 2 && otherDir == 3) || (openingDirection == 3 && otherDir == 2))
                    // {
                    //     // need left and bottom door
                    //     temproom = templates.getRoom("LB");
                    //     madeRoom = true;
                    // } else if ((openingDirection == 2 && otherDir == 4) || (openingDirection == 2 && otherDir == 4))
                    // {
                    //     // need top and left door
                    //     temproom = templates.getRoom("TL");
                    //     madeRoom = true;
                    // } else if ((openingDirection == 3 && otherDir == 4) || (openingDirection == 3 && otherDir == 4))
                    // {
                    //     // need top and bottom door
                    //     temproom = templates.getRoom("TB");
                    //     madeRoom = true;
                    // } else if ((openingDirection == 1 && otherDir == 2) || (openingDirection == 1 && otherDir == 2))
                    // {
                    //     // need left and right door
                    //     temproom = templates.getRoom("LR");
                    //     madeRoom = true;
                    // }
                    // // destroy spawnpoints to not make extra door
                    // if (madeRoom)
                    // {
                    //     GameObject[] roomChildren = GetComponentsInChildren<GameObject>(true); 
                    //     foreach (GameObject child in roomChildren)
                    //     {
                    //         if (child.name != "Destroyer")
                    //         {
                    //             Destroy(child);
                    //         }
                    //     }
                    //     temproom.GetComponent<RoomSpawner>().waitTime = 0f;
                    // }
                    Instantiate(temproom, transform.position, Quaternion.identity);
                }
                spawned = true;

            }
            
            if (other.CompareTag("DoorSpawner"))
            {
                other.GetComponent<DoorSpawner>().doorDirections.Add(openingDirection);
            }
        }
    }

}
