using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAK
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance;
        public int currentLevel;
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
            SceneManager.LoadScene("MainMenu");
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
            SceneManager.LoadScene(currentLevel); 
        }

        public void LoadSaved()
        {
            Debug.Log("This button will either load last saved game");
        }


    }
}

