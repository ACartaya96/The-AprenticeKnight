﻿using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {

    public InputControls Current;
    public Vector2 RightStickMultiplier = new Vector2(3, -1.5f);

	// Use this for initialization
	void Start () {
        Current = new InputControls();
	}

	void Update () {
        
        // Retrieve our current WASD or Arrow Key input
        // Using GetAxisRaw removes any kind of gravity or filtering being applied to the input
        // Ensuring that we are getting either -1, 0 or 1
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector2 rightStickInput = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));

        // pass rightStick values in place of mouse when non-zero
        mouseInput.x = rightStickInput.x != 0 ? rightStickInput.x * RightStickMultiplier.x : mouseInput.x;
        mouseInput.y = rightStickInput.y != 0 ? rightStickInput.y * RightStickMultiplier.y : mouseInput.y;

        bool jumpInput = Input.GetButtonDown("Jump");

        Current = new InputControls
    ()
        {
            MoveInput = moveInput,
            MouseInput = mouseInput,
            JumpInput = jumpInput
        };
	}
}

public struct InputControls 
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
}
