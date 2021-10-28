using UnityEngine;

public class FlashLightToggle : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    bool isOpen = false;

    private void ToggleLight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isOpen = !isOpen;
        }
    }

    
    
    private void Update()
    {
        ToggleLight();
        if(isOpen)
        {
            flashLight.SetActive(true);
        }
        else
        {
            flashLight.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Battery"))
        {
            isOpen = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            isOpen = false;

        }
    }
}
