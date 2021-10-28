using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    

    private void Start()
    {
        gameOverCanvas.enabled = false;
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("EscapeBoat"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void MenuLoader()
    {
        gameOverCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; //Stop the game when game over canvas active
        GetComponent<playerRotation>().enabled = false;
        GetComponent<playerMovement>().enabled = false;
        GetComponentInChildren<WeaponSwitch>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    
}
