using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    // Going to be used for saving player data
    public static int health = 100;
    public static string[] items;

    public static int[] eggCombinations = { 0, 0, 0, 0 };

    public static List<Quest> quests;
}
