using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TAK
{
    
    public class MainMenu : MonoBehaviour
{ 
    public void PlayGame()
    {
        SceneManager.LoadScene("Swamp");
        //Debug.Log("Start Game");
    }
    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Quit Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
}

