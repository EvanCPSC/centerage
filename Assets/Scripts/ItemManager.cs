using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string effect;
    public float value;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void GiveStat()
    {
        switch (effect)
        {
            case "speed":
                PlayerStats.playerSpeed += value;
                break;
            case "pickspeed":
                PlayerStats.pickaxeSpeed += value;
                break;
            case "damage":
                PlayerStats.pickaxeDamage += value;
                break;
            case "range":
                PlayerStats.pickaxeRange += value;
                break;
            case "health":
                PlayerStats.playerHealth += value;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            PlayerStats.pickaxeReturning = true;
            GiveStat();
            Destroy(gameObject);
        }
    }
}
