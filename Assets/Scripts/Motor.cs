using UnityEngine;

public class Motor : MonoBehaviour
{
    private float _currentRPM = 0;

    [SerializeField] private float _minRPM = 0;
    [SerializeField] private float _maxRPM = 7000;
    [SerializeField] private const int _motorTimeConstant = 2; // ms
    [SerializeField] private bool _cw = true; // Clockwise rotation
    [SerializeField] private float _thrustCoefficient = 1.91e-6f; // N/(RPM)^2
    [SerializeField] private float _torqueCoefficient = 2.6e-7f; // Nm/(RPM)^2

    public float CurrentRPM => _currentRPM;

    private void FixedUpdate()
    {
        
    }

    public void SetRPM(float targetRPM)
    {
        var error = targetRPM - _currentRPM;
        float deltaRPM = (error / _motorTimeConstant) * Time.fixedDeltaTime;
        _currentRPM += deltaRPM;
    }

    public float GetThrust()
    {
        return _thrustCoefficient * _currentRPM * _currentRPM;
    }

    public float GetTorque()
    {
        var torque = _torqueCoefficient * _currentRPM * _currentRPM;
        return _cw ? -torque : torque;
    }
}
