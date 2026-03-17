using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public string[] effect;
    public float[] value;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void GiveStat()
    {
        for (int i = 0; i < effect.Length; i ++) {
            switch (effect[i])
            {
                case "speed":
                    PlayerStats.playerSpeed += value[i];
                    break;
                case "pickspeed":
                    PlayerStats.pickaxeSpeed += value[i];
                    break;
                case "damage":
                    PlayerStats.pickaxeDamage += value[i];
                    break;
                case "damagemult":
                    PlayerStats.pickaxeDamage *= value[i];
                    break;
                case "range":
                    PlayerStats.pickaxeRange += value[i];
                    break;
                case "health":
                    PlayerStats.playerHealth += value[i];
                    break;
                default:
                    break;
            }
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
