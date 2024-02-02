using UnityEngine;

// SCRIPT VALIDATED. GOOD PRACTICES. APROVED.
public class MovementController : MonoBehaviour
{
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravityMultiplier = 2f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float rotationSpeed = 360;

    private CharacterController cc;
    private Vector3 inputDirection;
    private Vector3 jumpVelocity;
    private float gravityApplied;
    private int remainingJumps;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();   
    }

    private void Start()
    {
        gravityApplied = Physics.gravity.y * gravityMultiplier;

        remainingJumps = maxJumps;

    }

    //public method to set the movement direction make input system independnet
    public void SetMoveDirection(Vector3 direction)
    {
        inputDirection = direction;
    }

    public void Jump()
    {
        if (cc.isGrounded || (!cc.isGrounded && remainingJumps > 0))
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityApplied);
            remainingJumps--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ApplyGravity();
    }

    void Move() {
        //clamp the magnitude of this movement vector fixes de probLem with diagonal velocity and avoids 
        // the strange behavior that apears mixing GetAxis and Normalizing vectors.
        Vector3 movement = Vector3.ClampMagnitude(inputDirection, 1f);

        //com a alternativa al vector de moviment li afegir la rotació de la càmera principal,
        //així sempre funcionarà bé sigui quina sigui la rotació de la càmera
        movement = Quaternion.Euler(0, Camera.main.gameObject.transform.eulerAngles.y, 0) * movement;

        cc.Move(movement * speed * Time.deltaTime);

        Rotate(movement.normalized);
    }

    void ApplyGravity()
    {
        cc.Move(jumpVelocity * Time.deltaTime);

        if (cc.isGrounded && jumpVelocity.y < 0f)
        {
            remainingJumps = maxJumps;
            jumpVelocity.y = -1f;
        }
        else
        {
            jumpVelocity.y += gravityApplied * Time.deltaTime;
        }
        
    }

    /// <summary>
    /// Aquest mètode fa la seva màgia en dues línies per rotar el transform suaument i a certa
    /// velocitat cap a la direcció del moviment
    /// </summary>
    private void Rotate(Vector3 moveDirection)
    {
        if (moveDirection.magnitude <= 0) return;

        // Obtenim la rotació referent a una direcció de moviment. El moviment en que es mourà el personatge.
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Interpolem la rotació des de la rotació actual del transform cap a la rotació objectiu
        // uns quants graus cada frame. La velocitat de rotació dependrà de 'rotationSpeed'
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}