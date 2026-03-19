using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerStats
{
    public static bool isPaused = false;
    public static float playerSpeed = 5f;
    public static float playerHealth = 6f;
    public static float pickaxeSpeed = 8f;
    public static float pickaxeDamage = 3.5f;
    public static float pickaxeRange = 4f;
    public static bool pickaxeReturning = false;
    public static List<string> playerItems = new List<string>(); // for later implementation
    private static float defPlayerSpeed = 5f;
    private static float defPlayerHealth = 6f;
    private static float defPickaxeSpeed = 8f;
    private static float defPickaxeDamage = 3.5f;
    private static float defPickaxeRange = 4f;

    public static void SetDefaultStats()
    {
        playerSpeed = defPlayerSpeed;
        playerHealth = defPlayerHealth;
        pickaxeSpeed = defPickaxeSpeed;
        pickaxeDamage = defPickaxeDamage;
        pickaxeRange = defPickaxeRange;
        pickaxeReturning = false;
        playerItems = new List<string>();
        isPaused = false;
    }
}