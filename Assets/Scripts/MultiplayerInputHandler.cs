using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerInputHandler : MonoBehaviour
{
    [SerializeField] private Light llinterna;

    private MovementController movementController;
    private ShootController shootController;

    private InputActionAsset playerControls;
    private InputActionMap playerControlsMap;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        shootController = GetComponent<ShootController>();
    }

    private void OnEnable()
    {
        playerControls = GetComponent<PlayerInput>().actions;
        playerControlsMap = playerControls.FindActionMap("HumanControls");

        playerControlsMap.Enable();

        playerControlsMap.FindAction("Jump").performed += Jump;
        playerControlsMap.FindAction("Shoot").performed += Shoot;
        playerControlsMap.FindAction("Move").performed += Move;
        playerControlsMap.FindAction("Move").canceled += Move;
        playerControlsMap.FindAction("Lantern").performed += MakeLight;
        playerControlsMap.FindAction("Lantern").canceled += MakeLight;
    }

    private void OnDisable()
    {
        playerControlsMap.FindAction("Jump").performed -= Jump;
        playerControlsMap.FindAction("Shoot").performed -= Shoot;
        playerControlsMap.FindAction("Move").performed -= Move;
        playerControlsMap.FindAction("Move").canceled -= Move;
        playerControlsMap.FindAction("Lantern").performed -= MakeLight;
        playerControlsMap.FindAction("Lantern").canceled -= MakeLight;

        playerControlsMap.Disable();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        movementController.Jump();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        shootController.Shoot();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 vector = context.ReadValue<Vector2>();
        movementController.SetMoveDirection(new Vector3(vector.x, 0, vector.y));
    }

    private void MakeLight(InputAction.CallbackContext context)
    {
        llinterna.gameObject.SetActive(true);

        float intensityLantern = 33f;
        float angleLantern = 50f;

        float variablePere = context.ReadValue<float>();

        llinterna.intensity = intensityLantern * variablePere;
        llinterna.spotAngle = angleLantern * variablePere;
        llinterna.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 0, 1), variablePere);

        Debug.Log(variablePere);

        if (llinterna.intensity <= 0f)
        {
            llinterna.gameObject.SetActive(false);
        }
    }
}
