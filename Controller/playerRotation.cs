using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    [SerializeField] Transform orientation;

    Camera cam;
    WallRun wallrun;
    

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;



    private void Start()
    {
        wallrun = GetComponent<WallRun>();
        
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        GetInputs();
       
      
    }
   
    void GetInputs()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multiplier;
        xRotation -= mouseY * sensitivityY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //camera and player will move  x rotation on it own localrotation
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, wallrun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
