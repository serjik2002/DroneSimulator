using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneModel : MonoBehaviour
{
    [SerializeField] private int mass = 1200;
    [SerializeField] private float _shoulderLength = 0.5f;
    [SerializeField] private Motor[] motors;
    
    private int[,] _inertionMatrix;


    private void FixedUpdate()
    {
        var tractiveForce = 0f;
    }


}
