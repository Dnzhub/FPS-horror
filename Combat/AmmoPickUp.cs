using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] AmmoType ammoType;

    [SerializeField] Text AmmoText;

    private void Start()
    {
        AmmoText.enabled = false;
        
    }
    private void OnTriggerStay(Collider other)
    {
        AmmoText.text = "Pick Up " + gameObject.name;
        if (other.gameObject.CompareTag("Player"))
        {
            AmmoText.enabled = true;
            if (Input.GetKey(KeyCode.E))
            {
                Ammo ammo = other.GetComponent<Ammo>();
                ammo.increaseCurrentAmmo(ammoAmount, ammoType);
                AmmoText.enabled = false;
                Destroy(gameObject);
                
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            AmmoText.enabled = false;
    }
}
