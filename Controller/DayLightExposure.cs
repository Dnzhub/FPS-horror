using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightExposure : MonoBehaviour
{
    private void MorningLight()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 1);
    }
    private void NightLight()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 0.3f);
    }
}
