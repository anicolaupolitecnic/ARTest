using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandlerOld : MonoBehaviour
{
    [SerializeField] private Light llinterna;

    private MovementController movementController;
    private ShootController shootController;

    private PlayerControls playerControls;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        shootController = GetComponent<ShootController>();
    }

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.HumanControls.Enable();

        playerControls.HumanControls.Jump.performed += Jump;
        playerControls.HumanControls.Shoot.performed += Shoot;
        playerControls.HumanControls.Move.performed += Move;
        playerControls.HumanControls.Move.canceled += Move;
        playerControls.HumanControls.Lantern.performed += MakeLight;
        playerControls.HumanControls.Lantern.canceled += MakeLight;
    }

    private void OnDisable()
    {
        playerControls.HumanControls.Jump.performed -= Jump;
        playerControls.HumanControls.Shoot.performed -= Shoot;
        playerControls.HumanControls.Move.performed -= Move;
        playerControls.HumanControls.Move.canceled -= Move;
        playerControls.HumanControls.Lantern.performed -= MakeLight;
        playerControls.HumanControls.Lantern.canceled -= MakeLight;

        playerControls.HumanControls.Disable();
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

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Fire1")) {
    //        shootController.Shoot();
    //    }

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        movementController.Jump();
    //    }

    //    float xMovement = Input.GetAxis("Horizontal");
    //    float zMovement = Input.GetAxis("Vertical");
    //    movementController.SetMoveDirection(new Vector3(xMovement, 0, zMovement));
    //}
}