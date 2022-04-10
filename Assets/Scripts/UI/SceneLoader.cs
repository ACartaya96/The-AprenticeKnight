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
        public string currentLevel;
        public Camera camera;
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

            camera = Camera.main;

            DontDestroyOnLoad(instance);
            DontDestroyOnLoad(camera);
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
            camera.GetComponent<AudioListener> ().enabled  =  false;
        }

        public void LoadSwampScene()
        {
            SceneManager.LoadScene("Swamp");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            camera.GetComponent<AudioListener> ().enabled  =  false;
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


    }
}

