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
        
        //Debug.Log("Start Game");
    }
    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Quit Game");
    }

    public void LoadMainMenu()
    {
        //SceneLoader.LoadScene("MainMenu");
    }
    
    
}
}

