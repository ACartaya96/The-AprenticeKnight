using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAK
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance;

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

        public void LoadWinState()
        {
            SceneManager.LoadScene("WinScene");
        }

        public void LoadHubScene()
        {
            SceneManager.LoadScene("Hub");
        }

        public void LoadSwampScene()
        {
            SceneManager.LoadScene("Swamp");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
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
          
        }
    }
}

