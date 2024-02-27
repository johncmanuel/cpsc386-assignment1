using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Most of the menu functionality can be implemented in a service responsible for UI.
    // That will be worked upon later in the development cycle.
    // - John

    public void PlayGame()
    {
        Debug.Log("Playing game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
