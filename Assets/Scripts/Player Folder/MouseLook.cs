using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Transform PlayerRb;
    [SerializeField] private float Sensitivity;
    private Vector2 PlayerMouseInput;
    private float xRotation;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }
    private void CameraMovement() 
    {
        // grab mouse input
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //mutilply sens and look rotation
        xRotation -= PlayerMouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotation of camera and body
        PlayerRb.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
