using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneModel : MonoBehaviour
{
    [SerializeField] private int mass = 1200;
    [SerializeField] private float _shoulderLength = 0.5f;
    [SerializeField] private Motor[] _motors;


    [SerializeField] private float _rpm;
    
    private int[,] _inertionMatrix;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.mass = mass;
        _rigidbody.inertiaTensor = new Vector3(1, 1, 1);
        _rigidbody.inertiaTensorRotation = Quaternion.identity;
    }



    private void FixedUpdate()
    {
        TestSetRpm(_rpm);

        var thrust = GetThrust();
        _rigidbody.AddForce(thrust, ForceMode.Force);

        ApplyTotalTorque();
    }

    public void TestSetRpm(float rpm)
    {
        foreach (var motor in _motors)
        {
            motor.SetRPM(rpm);
        }
    }

    public Vector3 GetThrust()
    {
        var totalThrustOfMotors = 0f;
        foreach (var motor in _motors)
        {
            totalThrustOfMotors += motor.GetThrust();
        }
        float linearDragCoefficient = 0.1f;

        Vector3 localThrustVector = new Vector3(0, totalThrustOfMotors, 0);
        Vector3 globalThrustVector = transform.TransformDirection(localThrustVector);
        Vector3 dragForce = -linearDragCoefficient * _rigidbody.linearVelocity;
        Vector3 resultantForce = globalThrustVector + dragForce;

        return resultantForce;
    }

    private void ApplyTotalTorque()
    {
        float roll = (_motors[0].GetTorque() + _motors[2].GetTorque() - _motors[1].GetTorque() - _motors[3].GetTorque())*_shoulderLength;
        float pitch = (_motors[0].GetTorque() + _motors[1].GetTorque() - _motors[2].GetTorque() - _motors[3].GetTorque())*_shoulderLength;
        float yaw = _motors[0].GetTorque() - _motors[1].GetTorque() + _motors[2].GetTorque() - _motors[3].GetTorque();

        Vector3 torqueVector = new Vector3(roll, yaw, pitch);
        _rigidbody.AddRelativeTorque(torqueVector, ForceMode.Force);
    }

}
