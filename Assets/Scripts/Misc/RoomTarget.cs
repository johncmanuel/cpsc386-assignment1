using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTarget : MonoBehaviour
{
    private string roomName;

    private void Start()
    {
        roomName = gameObject.transform.parent.name;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("Spawning enemies in: " + roomName);
            // Spawn enemies in the room
            GameManager.Instance.SpawnEnemies(roomName);
        }
    }
}
