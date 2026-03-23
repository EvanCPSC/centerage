using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PlayerStats
{
    // stats (player stats and game states)
    public static bool isPaused = false;
    public static float playerSpeed = 5f;
    public static float playerHealth = 6f;
    public static float pickaxeForce = 8f;
    public static float pickaxeDamage = 3.5f;
    public static float pickaxeRange = 4f;
    public static bool pickaxeReturning = false;
    public static bool pickaxeRetrieved = false;
    public static List<string> playerItems = new List<string>(); // for later implementation
    private static float defPlayerSpeed = 5f;
    private static float defPlayerHealth = 6f;
    private static float defPickaxeForce = 8f;
    private static float defPickaxeDamage = 3.5f;
    private static float defPickaxeRange = 4f;

    public static void SetDefaultStats()
    {
        playerSpeed = defPlayerSpeed;
        playerHealth = defPlayerHealth;
        pickaxeForce = defPickaxeForce;
        pickaxeDamage = defPickaxeDamage;
        pickaxeRange = defPickaxeRange;
        pickaxeReturning = false;
        playerItems = new List<string>();
        isPaused = false;
        SetDefaultItemBools();
    }

    // item bools (if they have a certain item or not)
    public static bool dioptase = false;
    public static bool dioptaseUsed = false;
    public static bool labradorite = false;
    public static bool pearl = false;
    public static bool pearlAlt = false;
    public static float pearlRange = 0.2f; // const

    public static void SetDefaultItemBools()
    {
        dioptase = false;
        dioptaseUsed = false;
        labradorite = false;
        pearl = false;
        pearlAlt = false;
    }
}