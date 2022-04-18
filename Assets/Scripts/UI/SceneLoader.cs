using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

namespace TAK
{
    public class SceneLoader : MonoBehaviour
    {
        public static int currentLevel;
        public static SceneLoader instance;
        Camera cam;
    
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            cam = Camera.main;

            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(cam);

        }

        

        public void LoadWinScene()
        {
            SceneManager.LoadScene("WinScene");
        }
        internal void LoadLoseScene()
        {

            SceneManager.LoadScene("LoseScene");
        }
        public void LoadHubScene()
        {
            SceneManager.LoadScene("Hub");
            cam.GetComponent<AudioListener> ().enabled  =  false;
        }

        public void LoadSwampScene()
        {
            SceneManager.LoadScene("Swamp");
        }

        public void LoadMainMenu()
        {
            SceneLoader.instance.TriggerLoadLevel("MainMenu");
            cam.GetComponent<AudioListener> ().enabled  =  false;
        }


        public void LoadQuit()
        {
            Application.Quit();
        }

        public void TriggerLoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        public void RestartLevel()
        {
           
            if(currentLevel == 1)
            {   
                SceneLoader.instance.TriggerLoadLevel("Hub");
            }
            else if (currentLevel == 2)
            {
                SceneLoader.instance.TriggerLoadLevel("Tutorial");
            }

            else if (currentLevel == 3)
            {
                SceneLoader.instance.TriggerLoadLevel("Swamp");
            }
            else if (currentLevel == 4)
            {
                SceneLoader.instance.TriggerLoadLevel("Mountain");
            } 
            else if (currentLevel == 5)
            {
                SceneLoader.instance.TriggerLoadLevel("Lair");
            }
            else
            {
                print ("The Restart is not Working!!! dummy  BAKA");
            }
        }

        public void LoadSaved()
        {
            Debug.Log("This button will either load last saved game");
        }


    }
}

