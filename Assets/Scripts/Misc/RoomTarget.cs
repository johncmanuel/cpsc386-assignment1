using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Mainly used for spawning enemies in a room when the player enters it
public class RoomTarget : MonoBehaviour
{
    private string roomName;

    private void Start()
    {
        // Assumes that the parent of the RoomTarget 
        // is game object with the name of the room
        roomName = gameObject.transform.parent.name;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("Spawning enemies in: " + roomName);
            EnemyManager.Instance.SpawnEnemies(roomName);
        }
    }
}
