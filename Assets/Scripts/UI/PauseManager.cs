using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace TAK
{
public class PauseManager : MonoBehaviour
{
    
    public static bool paused = false;

    public GameObject pauseMenu, htpMenu, optMenu, ctrlMenu;

    public GameObject htpBtn, optBtn, ctrlBtn, htpbackBtn, optbackBtn, masterSld, menuBtn, quitBtn;

    public PauseAction action;

    //InputHandler inputHandler;


    PlayerInput playerInput;


    private void Awake()
    {
        //eventSystem = FindObjectOfType<EventSystem>();
        //inputHandler = FindObjectOfType<InputHandler>();
        action = new PauseAction();
        if(paused)
        ResumeGame();
    }

    private void OnEnable()
    {
        action.Enable();
        //inputHandler.PauseAction.started += _ => PauseGame();
        //inputHandler.UnpauseAction.started += _ => ResumeGame();
    }

    private void OnDisable()
    {
       action.Disable();
    }


    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
        
    }

    private void DeterminePause()
    {
        if (paused)
            ResumeGame();
        else    
            PauseGame();    
    }





    public void PauseGame()
    {
        Time.timeScale = 0;
        //AudioListener.pause = true;
        paused = true;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(htpBtn);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //AudioListener.pause = false;
        paused = false;
        pauseMenu.SetActive(false);
        htpMenu.SetActive(false);
        ctrlMenu.SetActive(false);
    }


    public void OpenHTPMenu()
    {
        pauseMenu.SetActive(false);
        htpMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ctrlBtn);
    }

    public void CloseHTPMenu()
    {
        pauseMenu.SetActive(true);
        htpMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(htpBtn);
    }

    public void OpenOPTMenu()
    {
        pauseMenu.SetActive(false);
        optMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optbackBtn);
    }

    public void CloseOPTMenu()
    {
        pauseMenu.SetActive(true);
        optMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optBtn);
    }


    public void OpenCTRLMenu()
    {
        htpMenu.SetActive(false);
        ctrlMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(masterSld);
    }

    public void CloseCTRLMenu()
    {
        htpMenu.SetActive(true);
        ctrlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ctrlBtn);
    }


    public void LoadMainMenu()
    {
            SceneLoader.instance.LoadMainMenu();
    }

    public void QuitGame()
    {
            SceneLoader.instance.LoadQuit();
    }
}
}