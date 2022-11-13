using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCycle : MonoBehaviour
{
    public static bool p;

    public void setDay()
    {
        p = true;
    }

    public void setNight()
    {
        p = false;
    }
}
