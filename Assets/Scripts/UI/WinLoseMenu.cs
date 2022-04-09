using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


namespace TAK
{
    
    public class WinLoseMenu : MonoBehaviour
{ 
    //The Menu screens themselves
    public GameObject winMenu, credMenu;

    //The Buttons in each of the menus
    public GameObject menuBtn, credBtn, credbackBtn;
    
    
    void Start()
    {
        //Deselecting all buttons as protocol
        EventSystem.current.SetSelectedGameObject(null);
        //Making the Start Button the first selected button
        EventSystem.current.SetSelectedGameObject(menuBtn);
    }

    public void OpenCREDMenu()
    {
        winMenu.SetActive(false);
        credMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(credbackBtn);
    }

    public void CloseCREDMenu()
    {
        winMenu.SetActive(true);
        credMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(credBtn);
    }

    
























}
}

