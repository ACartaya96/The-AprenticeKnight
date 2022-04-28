using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TAK
{
    
    public class MainMenu : MonoBehaviour
{ 
    //The Menu screens themselves
    public GameObject mainMenu, htpMenu, optMenu, ctrlMenu, credMenu;

    //The Buttons in each of the menus
    public GameObject strBtn, htpBtn, optBtn, masterSld, ctrlBtn, credBtn, ctrlbackBtn, credbackBtn;

    public Toggle Healthtog;
    public Toggle Manatog;
    
    
    void Start()
    {
        //Deselecting all buttons as protocol
        EventSystem.current.SetSelectedGameObject(null);
        //Making the Start Button the first selected button
        EventSystem.current.SetSelectedGameObject(strBtn);
        if(SceneLoader.instance.InfiniteMana == true)
        {
            Manatog.isOn = true;
        }
        else
        {
            Manatog.isOn = false;
        }
        if(SceneLoader.instance.InfiniteHealth == true)
        {
            Healthtog.isOn = true;
        }
        else
        {
            Healthtog.isOn = false;
        }
    }
    
    public void StartGame()
     {
       SceneLoader.instance.LoadHubScene();
     }

    public void QuitGame()
    {
       SceneLoader.instance.LoadQuit();
    }
    public void OpenHTPMenu()
    {
        mainMenu.SetActive(false);
        htpMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ctrlBtn);
    }

    public void CloseHTPMenu()
    {
        mainMenu.SetActive(true);
        htpMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(htpBtn);
    }

    public void OpenOPTMenu()
    {
        mainMenu.SetActive(false);
        optMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(masterSld);
    }

    public void CloseOPTMenu()
    {
        mainMenu.SetActive(true);
        optMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optBtn);
    }

    public void OpenCTRLMenu()
    {
        htpMenu.SetActive(false);
        ctrlMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ctrlbackBtn);
    }

    public void CloseCTRLMenu()
    {
        htpMenu.SetActive(true);
        ctrlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ctrlBtn);
    }

    public void OpenCREDMenu()
    {
        htpMenu.SetActive(false);
        credMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(credbackBtn);
    }

    public void CloseCREDMenu()
    {
        htpMenu.SetActive(true);
        credMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(credBtn);
    }
     public void HealthCheat(bool Toggle)
        {
            if(Toggle == true)
            {
                SceneLoader.instance.InfiniteHealth = true;
            }
           else
            {
                SceneLoader.instance.InfiniteHealth = false;
            }
        }
        public void ManaCheat(bool Toggle)
        {
          if(Toggle == true)
           {
               SceneLoader.instance.InfiniteMana = true;
           }
           else
           {
               SceneLoader.instance.InfiniteMana = false;
           }
        }

}
}

