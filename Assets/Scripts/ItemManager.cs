using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public string itemName;
    public string itemDesc;
    public int quality;
    public string[] effect;
    public float[] value;
    public AudioManager audioManager;
    public GameManager gameManager;

    void Start()
    {
        audioManager = AudioManager.Instance;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GiveStat()
    {
        for (int i = 0; i < effect.Length; i ++) {
            switch (effect[i])
            {
                case "speed":
                    PlayerStats.playerSpeed += value[i];
                    break;
                case "force":
                    PlayerStats.pickaxeForce += value[i];
                    break;
                case "forcemult":
                    PlayerStats.pickaxeForce *= value[i];
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
                case "labradorite":
                    PlayerStats.labradorite = true;
                    break;
                case "pearl":
                    PlayerStats.pearl = true;
                    break;
                case "dioptase":
                    PlayerStats.dioptase = true;
                    PlayerStats.playerAbilities.Add("dioptase");
                    break;
                case "moonstone":
                    PlayerStats.moonstone = true;
                    PlayerStats.playerAbilities.Add("moonstone");
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
            PlayerStats.playerItems.Add(itemName);
            audioManager.PlaySFX(audioManager.sfxGetItem);
            gameManager.ShowItemGet(itemName, itemDesc);
            GiveStat();
            Destroy(gameObject);
        }
    }
}
