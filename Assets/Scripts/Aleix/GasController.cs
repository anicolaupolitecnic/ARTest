using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GasController : MonoBehaviour
{
    [SerializeField] private Transform gasPointer;
    [SerializeField] private float speedGasLost = 0.01f;
    
    
    private float accelerationGasLost = 1;

    private float maxGasAngle = -49;
    private float minGasAngle = 137;
    private float currentGasAngle;
    private float angleDifference;

    private void OnEnable()
    {
        AccelerationController.Accelerate += WasteGas;
        AccelerationController.StopAccelerate += StopWasteGas;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGasAngle = maxGasAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (gasPointer != null && currentGasAngle < minGasAngle)
        {
            currentGasAngle += speedGasLost * Time.deltaTime * accelerationGasLost;
            angleDifference = currentGasAngle - gasPointer.eulerAngles.z;
            gasPointer.Rotate(0, 0, angleDifference);
        }

    }

    private void WasteGas()
    {
        accelerationGasLost = 10;
        Debug.Log(accelerationGasLost);
    }
    
    private void StopWasteGas()
    {
        accelerationGasLost = 1;
        Debug.Log(accelerationGasLost);
    }
}
