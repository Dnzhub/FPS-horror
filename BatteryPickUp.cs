using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryPickUp : MonoBehaviour
{
    [SerializeField] float restoreAngle = 120f;
    [SerializeField] float restoreIntensity = 30f;
    

    [SerializeField] Text pickUpText;

    private void Start()
    {
        pickUpText.enabled = false;
        pickUpText.text = "Pick up " + gameObject.name;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUpText.enabled = true;

            if(Input.GetKey(KeyCode.E))
            {
                FlashLightController flashLight = other.gameObject.GetComponentInChildren<FlashLightController>();
                flashLight.IncreaseAngle(restoreAngle);
                flashLight.IncreaseIntensity(restoreIntensity);
                pickUpText.enabled = false;               
                Destroy(gameObject);
               
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            pickUpText.enabled = false;
    }

}
