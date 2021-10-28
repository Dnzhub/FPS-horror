using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControllerMovement : MonoBehaviour
{
    //--References--//
    CharacterController controller;
    

    [Header("Movement")]
    [SerializeField] private float WalkSpeed = 5;
    [SerializeField] private float RunSpeed = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    Vector3 velocity;
    float Speed;
    Vector3 lastPosition;

    [Header("Jump")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool IsGrounded;
    
  


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerMovement();
        Jump();
        foodStep();
       


    }
    void foodStep()
    {
        if(IsGrounded && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            SoundManager.instance.playFoodStep();
        }
        else
        {
            SoundManager.instance.stopFoodStep();
        }
       
    }

    void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //--Reset y value if character on ground--//
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 moveDirection = transform.right * x + transform.forward * z;

        //--Fix for moving diagonal faster--//
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);


        controller.Move(moveDirection * Speed * Time.deltaTime);

        //--Apply the gravity--//
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //--Walk Run--//
        if(Input.GetKey(KeyCode.LeftShift) && IsGrounded)
        {
            Speed = RunSpeed;
            SoundManager.instance.runSound();
        }
        else
        {
            Speed = WalkSpeed;
            SoundManager.instance.walkSound();
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
