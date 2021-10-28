using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodyMovement : MonoBehaviour
{
    private Vector3 movementInputs;
    private Vector2 MouseInput;
    Camera mainCam;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    public float sensitivity;
    float xRot;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        movementInputs = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        playerMove();
        playerRotation();

    }

    private void playerMove()
    {
        Vector3 moveVector = transform.TransformDirection(movementInputs) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(groundCheck.position,0.4f,groundMask))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
            

        }

        
    }
    private void playerRotation()
    {
        xRot -= MouseInput.y * sensitivity;

        transform.Rotate(0f, MouseInput.x * sensitivity,0f);
        mainCam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
