using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace TAK
{
public class PauseManager : MonoBehaviour
{
    
    public static bool paused = false;

    public GameObject pauseMenu;
    PauseAction action;

    InputHandler inputHandler;

    //PlayerInput playerInput;

    //InputAction pauseAction;

    private void Awake()
    {
        action = new PauseAction();
        inputHandler = GetComponentInParent<InputHandler>();
        //pauseAction = playerInput.actions["Pause"];
    }

    private void OnEnable()
    {
        action.Enable();
        //pauseAction.started += _ => paused = true;
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
            //playerInput.SwitchCurrentActionMap("UI");
            ResumeGame();
        else    
            PauseGame();    
    }





    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        paused = true;
        pauseMenu.SetActive(true);
        inputHandler.playerInput.actions.Disable();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        paused = false;
        pauseMenu.SetActive(false);
        inputHandler.playerInput.actions.Enable();
    }











}
}