using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _mainThrust = 1f;
    [SerializeField] private float _rotatingThrust = 1f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(_rotatingThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-_rotatingThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        }
    }
}