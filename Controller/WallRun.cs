using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    //--Note: For smooth result: should add to player physics material on collider set values 0 and friction combine = Minimum --//
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] float wallRunGravity;
    [SerializeField] float wallJumpForce;


    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunFov;
    [SerializeField] private float wallRunFovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;


    Weapon weapon;
    public bool isOnWall = false;

    public float tilt {get; private set;}

    Rigidbody rb;
    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        rb = GetComponent<Rigidbody>();
    }

   
    
    void Update()
    {     
        //changeCameraFov();
       
               
        wallCheck();
        if(canWallRun())
        {
            if(wallLeft)
            {
                startWallRun();
                
            }
            else if(wallRight)
            {
                startWallRun();
                
            }
            else
            {
                stopWallRun();
            }
            
        }        
        else
        {
            stopWallRun();
        }
    }
   
    bool canWallRun()
    {
        //--Ýf raycast doesnt hit ground it means we are on air return true--//
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight); //!playerMovement.isGrounded;
    }

    void wallCheck()
    {


        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }
    void startWallRun()
    {
        isOnWall = true;
        rb.useGravity = false;
       
        //Start wall run with own gravity(wallRunGravity)
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);       
        
       cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);
        
    
        if (wallLeft)
            //--Tilt to camera on the left--//
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if(wallRight)
            //--Tilt to camera on the right--//
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //--Wall jumping--//
            if(wallLeft)
            {
                //----When wall running if player jump it will jump on wall's normals direction----//
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallJumpForce * 100 , ForceMode.Force);
            }
            else if(wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallJumpForce * 100 , ForceMode.Force);
            }
        }
    }

    void stopWallRun()
    {
        isOnWall = false;
        rb.useGravity = true;
        
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }


}
