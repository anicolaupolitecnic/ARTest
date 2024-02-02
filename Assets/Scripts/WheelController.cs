using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightCol;
    [SerializeField] private WheelCollider frontLeftCol;
    [SerializeField] private WheelCollider backRightCol;
    [SerializeField] private WheelCollider backLeftCol;

    [SerializeField] private bool hasMeshWheels = true;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform backRightWheel;
    [SerializeField] private Transform backLeftWheel;

    [SerializeField] private Transform roll;

    [SerializeField] private bool frontTraction = true;
    [SerializeField] private bool backTraction = true;
    [SerializeField] private float accelerationForce = 500f;
    [SerializeField] private float backwardsAccelerationForce = 500f;
    [SerializeField] private float brakeingForce = 300f;
    [SerializeField] private float maxTurnAngle = 15f;

    [SerializeField] private Vector3 centerOfMassOffset;

    private float currentAcceleration = 0f;
    private float currentBackwardsAcceleration = 0f;
    private float currentBrakeing = 0f;
    private float currentTurnAngle = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.centerOfMass = centerOfMassOffset;
    }

    private void FixedUpdate()
    {
        // Apply acceleration to front wheels .
        if (frontTraction)
        {
            frontRightCol.motorTorque = GetAccelerationValue(frontRightCol);
            frontLeftCol.motorTorque = GetAccelerationValue(frontLeftCol);
        }

        // Apply acceleration to front wheels .
        if (backTraction)
        {
            backRightCol.motorTorque = GetAccelerationValue(backRightCol);
            backLeftCol.motorTorque = GetAccelerationValue(backLeftCol);
        }

        // Apply brakeing force to all wheels
        frontRightCol.brakeTorque = GetBrakeingValue(frontRightCol);
        frontLeftCol.brakeTorque = GetBrakeingValue(frontLeftCol);
        backRightCol.brakeTorque = GetBrakeingValue(backRightCol);
        backLeftCol.brakeTorque = GetBrakeingValue(backLeftCol);

        frontRightCol.steerAngle = currentTurnAngle;
        frontLeftCol.steerAngle = currentTurnAngle;

        if (hasMeshWheels)
        {
            UpdateWheel(frontRightCol, frontRightWheel);
            UpdateWheel(frontLeftCol, frontLeftWheel);
            UpdateWheel(backRightCol, backRightWheel);
            UpdateWheel(backLeftCol, backLeftWheel);
        }
    }

    private float GetAccelerationValue(WheelCollider wheelCollider)
    {
        if (currentBrakeing > 0 && wheelCollider.rpm < 0.001)
            return -currentBackwardsAcceleration;
        
        return currentAcceleration;
    }

    private float GetBrakeingValue(WheelCollider wheelCollider)
    {
        if (currentBrakeing > 0 && wheelCollider.rpm < 0.001)
            return 0;

        return currentBrakeing;
    }

    private void UpdateWheel(WheelCollider col, Transform wheel)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        wheel.position = position;
        wheel.rotation = rotation;

        if (roll != null)
            roll.localRotation = Quaternion.Euler(0, 0, -currentTurnAngle * 2);

    }

    public void SetAcceleration(float _acceleration)
    {
        currentAcceleration = accelerationForce * _acceleration;
    }

    public void SetBrakeing(float _brakeing)
    {
        currentBrakeing = brakeingForce * _brakeing;
        currentBackwardsAcceleration = backwardsAccelerationForce * _brakeing;
    }

    public void SetTurning(float _turning)
    {
        currentTurnAngle = maxTurnAngle * _turning;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position + transform.rotation * GetComponent<Rigidbody>().centerOfMass, .3f);
    //}
}
