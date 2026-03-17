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
    // public HashSet<Transform> roomLocs = new HashSet<Transform>();
    public float waitTime;
    private bool spawnedExit = false;
    public GameObject exit;

    void Update()
    {
        if (waitTime <= 0 && !spawnedExit)
        {
            int i = 0;
            while (i < rooms.Count)
            {
                // if (rooms[i].name == "NA(Clone)")
                // {
                //     // delete NA rooms
                //     GameObject naroom = rooms[i];
                //     rooms.RemoveAt(i);
                //     Destroy(naroom);
                //     i -= 1;
                //     continue;
                // }
                // if (roomLocs.Contains(rooms[i].transform))
                // {
                //     // delete duped rooms
                //     GameObject duperoom = rooms[i];
                //     rooms.RemoveAt(i);
                //     Destroy(duperoom);
                //     i -= 1;
                // } else
                // {
                //     roomLocs.Add(rooms[i].transform);
                // }
                if (i == rooms.Count-1)
                {
                    int j = i;
                    while (rooms[j].name.Length != 8 && j > 0)
                    {
                        j -= 1; // look for 1 door room
                    }
                    if (j < 4)
                    {
                        j = i; // if not found, just use last room (its happened)
                    }
                    Instantiate(exit, rooms[j].transform.position, Quaternion.identity);
                    spawnedExit = true;
                    break;
                }
                i += 1;
            }
        } else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public GameObject getRoom(string room)
    {
        if (room[0] == 'L')
        {
            for (int i = 0; i < leftRooms.Length; i++)
            {
                if (leftRooms[i].name == room)
                {
                    return leftRooms[i];
                }
            }
        } else if (room[0] == 'R')
        {
            for (int i = 0; i < rightRooms.Length; i++)
            {
                if (rightRooms[i].name == room)
                {
                    return rightRooms[i];
                }
            }
        } else if (room[0] == 'T')
        {
            for (int i = 0; i < topRooms.Length; i++)
            {
                if (topRooms[i].name == room)
                {
                    return topRooms[i];
                }
            }
        } else if (room[0] == 'B')
        {
            for (int i = 0; i < bottomRooms.Length; i++)
            {
                if (bottomRooms[i].name == room)
                {
                    return bottomRooms[i];
                }
            }
        }
        return closedRoom;
    }

}
