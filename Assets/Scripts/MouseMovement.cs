using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensivity = 200f;
    public Transform playerCamera;

    float xRotation = 0f;

    public float recoilSnappiness = 10f;
    public float recoilReturnSpeed = 5f;

    public float recoilCurrent = 0f;
    public float recoilTarget =  0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        recoilTarget = Mathf.Lerp(recoilTarget, 0f, recoilReturnSpeed * Time.deltaTime);
        recoilCurrent = Mathf.Lerp(recoilCurrent, recoilTarget, recoilSnappiness * Time.deltaTime);


        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation - recoilCurrent, 0f, 0f);

        
        transform.Rotate(Vector3.up * mouseX);

    }
    public void recoilVer(float recoilAmound)
    {
        recoilTarget += recoilAmound;
        
    }
}
