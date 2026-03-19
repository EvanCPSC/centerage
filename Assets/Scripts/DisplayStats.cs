using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI health, speed, pickspeed, damage, range;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = string.Format("{0:0.00}", PlayerStats.playerHealth);
        speed.text = string.Format("{0:0.00}", PlayerStats.playerSpeed);
        pickspeed.text = string.Format("{0:0.00}", PlayerStats.pickaxeSpeed);
        damage.text = string.Format("{0:0.00}", PlayerStats.pickaxeDamage);
        range.text = string.Format("{0:0.00}", PlayerStats.pickaxeRange);
    }
}
