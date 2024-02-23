using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private ContadorCarrera timer;

    private WheelController wheelController;

    private InputActions controls;

    private void Start()
    {
        timer = GameObject.Find("UI").GetComponent<ContadorCarrera>();
    }

    private void OnEnable()
    {
        wheelController = GetComponent<WheelController>();

        controls = new InputActions();
        controls.Car.Enable();

        controls.Car.Acceleration.performed += Accelerate;
        controls.Car.Acceleration.canceled += Accelerate;

        controls.Car.Brake.performed += Brake;
        controls.Car.Brake.canceled += Brake;

        controls.Car.Turn.performed += Turn;
        controls.Car.Turn.canceled += Turn;
    }

    private void OnDisable()
    {

        controls.Car.Acceleration.performed -= Accelerate;
        controls.Car.Acceleration.canceled -= Accelerate;

        controls.Car.Brake.performed -= Brake;
        controls.Car.Brake.canceled -= Brake;

        controls.Car.Turn.performed -= Turn;
        controls.Car.Turn.canceled -= Turn;

        controls.Car.Disable();
    }

    private void Accelerate(InputAction.CallbackContext ctx)
    {
        if (!timer.TimerOn)
        {
            wheelController.SetAcceleration(ctx.ReadValue<float>());
        }
    }

    private void Brake(InputAction.CallbackContext ctx)
    {
        if (!timer.TimerOn)
        {
            wheelController.SetBrakeing(ctx.ReadValue<float>());
        }    
    }

    private void Turn(InputAction.CallbackContext ctx)
    {
        if (!timer.TimerOn)
        {
            wheelController.SetTurning(ctx.ReadValue<float>());
        }  
    }

}