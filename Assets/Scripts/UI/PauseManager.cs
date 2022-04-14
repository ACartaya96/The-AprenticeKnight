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

    public GameObject pauseMenu, htpMenu, ctrlMenu;

    public GameObject htpBtn, ctrlBtn, htpbackBtn, ctrlbackBtn;

    PauseAction action;

    InputHandler inputHandler;

    PlayerInput playerInput;


    private void Awake()
    {
        action = new PauseAction();
    }

    private void OnEnable()
    {
        action.Enable();
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











}
}