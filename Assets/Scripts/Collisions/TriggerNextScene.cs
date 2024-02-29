using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerNextScene : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision detected with: " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            if (gameManager == null)
            {
                Debug.LogError("Game Manager not set in TriggerNextScene");
                return;
            }

            if (sceneName == null)
            {
                Debug.LogError("Scene Name not set in TriggerNextScene");
                return;
            }

            gameManager.SwitchToScene(sceneName);
        }
    }

}
