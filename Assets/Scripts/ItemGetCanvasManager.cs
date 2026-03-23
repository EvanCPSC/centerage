using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ItemGetCanvasManager : MonoBehaviour
{
    public TextMeshProUGUI itemText, descText;
    public string item, desc;

    void Start()
    {
        
    }

    void Update()
    {
        itemText.text = item;
        descText.text = desc;
    }


}