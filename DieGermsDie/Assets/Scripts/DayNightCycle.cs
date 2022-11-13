using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public GameObject directionalLight;
    public Color DayColor;
    public Color NightColor;

    public void Start() 
    {
        if (SetCycle.p)
        {
            RenderSettings.ambientLight = DayColor;
        }
        else if(!SetCycle.p)
        {
            RenderSettings.ambientLight = NightColor;
            directionalLight.transform.eulerAngles = new Vector3(directionalLight.transform.eulerAngles.x -90, directionalLight.transform.eulerAngles.y, directionalLight.transform.eulerAngles.z);
            directionalLight.GetComponent<Light>().intensity = 0;
        }

    }
}
