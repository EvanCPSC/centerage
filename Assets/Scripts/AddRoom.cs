using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;
    
    public GameObject spawnpoints;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
