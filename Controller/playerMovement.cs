using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 6f;
    float movementMultiplier = 10f;
    float airMultiplier = 0.3f;
    float horizontalMove;
    float verticalMove;
    Vector3 moveDirection;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float acceleration = 10f;


    public Transform orientation;

    [Header("Jump")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public bool isGrounded;
    float jumpForce = 13f;
    public LayerMask groundMask;


    [Header("Rigidbody Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2f;
   

   
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;




    CapsuleCollider capsuleCollider;
    Rigidbody rb;

    //----Note: For purpose of script working well set the gravity on Project Setting -> Physics - Gravity about -30
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
  
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        
        GetInputs();
        rigidbodyDrag();
        speedControl();
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }

        //--player will move this direction when on slope--//
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
    private void FixedUpdate()
    {
        MoveAction();
    }
    void speedControl()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
    private bool isSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit, capsuleCollider.height / 2 + 0.5f))
        {
            //--SlopeHit.normal means ground is straight so not hill or etc.
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }          
        }
        return false;
    }
    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
    void rigidbodyDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
      
    }

    void GetInputs()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.transform.forward * verticalMove + orientation.transform.right * horizontalMove;
    }

    void MoveAction()
    {
        //----Move direction should normalized else direct movements will bigger than diagonal moves----//
        if (isGrounded && !isSlope())
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        else if (isGrounded && isSlope())
            rb.AddForce(slopeMoveDirection.normalized * movementSpeed * 20 * airMultiplier, ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);

    }

}
