using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationController : MonoBehaviour
{

    public static event Action Accelerate;
    public static event Action StopAccelerate;


    public void AccelerateM()
    {
        Accelerate.Invoke();
    }

    public void StopAccelerateM()
    {
        StopAccelerate.Invoke();
    }
}
