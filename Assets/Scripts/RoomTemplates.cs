using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomTemplates : MonoBehaviour
{
    
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject closedRoom;

    public List<GameObject> rooms;
    public float waitTime;
    private bool spawnedExit = false;
    public GameObject exit;

    void Update()
    {
        if (waitTime <= 0 && !spawnedExit)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count-1)
                {
                    Instantiate(exit, rooms[i].transform.position, Quaternion.identity);
                    spawnedExit = true;
                }
            }
        } else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
