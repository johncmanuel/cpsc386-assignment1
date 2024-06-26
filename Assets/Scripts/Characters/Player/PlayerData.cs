using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    // Persist any needed player data in memory (i.e player position
    // after switching scenes). This is mainly used to 
    // easily save data to disk through some means.
    // Reference used:
    // https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes 

    public static Vector3 PlayerPosition { get; set; }
    public static float PlayerHealth { get; set; }
    public static IWeapon PlayerGun { get; set; }
    public static GameObject PlayerGunObj { get; set; }

    // Track last positions of the player in each scene before switching to the next one
    public static Dictionary<string, Vector3> PlayerPositions = new Dictionary<string, Vector3>();

    public static void ResetPlayerData()
    {
        Debug.Log("Resetting player data");
        PlayerPosition = Vector3.zero;
        PlayerHealth = 0;
        PlayerGun = null;
        PlayerGunObj = null;
    }
}