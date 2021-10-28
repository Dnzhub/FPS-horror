using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] float lightDecreaseValue = .1f;
    [SerializeField] float angleDecreaseValue = 1f;
    [SerializeField] float minimumAngleValue = 40f;

    [SerializeField] float maxAngleValue = 120f;
    [SerializeField] float maxIntensityValue = 30f;

    Light flashLight;
    void Start()
    {
        flashLight = GetComponent<Light>();
    }

    

    public void IncreaseAngle(float restoreValue)
    {
        flashLight.spotAngle += restoreValue;
        if (flashLight.spotAngle > maxAngleValue)
        {
            flashLight.spotAngle = maxAngleValue;
        }
      
        
    }
    public void IncreaseIntensity(float restoreIntensity)
    {
        flashLight.intensity += restoreIntensity;
        if(flashLight.intensity > maxIntensityValue)
        {
            flashLight.intensity = maxIntensityValue;
        }
    }
    
    private void decreaseAngle()
    {
        flashLight.spotAngle = Mathf.Max(flashLight.spotAngle - angleDecreaseValue * Time.deltaTime, minimumAngleValue);
    }
    private void decreaseIntensity()
    {
        flashLight.intensity -= lightDecreaseValue * Time.deltaTime;
    }

    void Update()
    {
        decreaseAngle();
        decreaseIntensity();
    }
}
