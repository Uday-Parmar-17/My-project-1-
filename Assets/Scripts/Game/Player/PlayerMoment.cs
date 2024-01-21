using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoment : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothmovementInput;
    private Vector2 _movementInputSmoothVelocity;
    [SerializeField]
    private float _rotationSpeeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RoatateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        _rigidbody.velocity = _smoothmovementInput * _speed; //Need to change this in future
        _smoothmovementInput = Vector2.SmoothDamp(_smoothmovementInput, _movementInput, ref _movementInputSmoothVelocity,
            0.1f);
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
    private void RoatateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothmovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeeed * Time.deltaTime);
            _rigidbody.MoveRotation(rotation);
        }
    }

}
