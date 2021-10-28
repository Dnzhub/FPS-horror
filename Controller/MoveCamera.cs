using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //--This script stops jittering while camera moving--//
    [SerializeField] Transform CameraPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraPosition.position;
    }
}
