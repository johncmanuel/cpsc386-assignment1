using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerNextScene : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision detected with: " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            gameManager.SwitchToScene("Dungeon2");
        }
    }

}
