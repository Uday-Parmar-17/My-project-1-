using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovemnet : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarness _playerAwarnessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarnessController = GetComponent<PlayerAwarness>();
        _targetDirection = transform.up;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

    }
    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();

        HandlePlayerTargeting();
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarnessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarnessController.DirectionToPlayer;
        }
    }
    private void RotateTowardsTarget()
    {
        
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation  = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _rigidbody.SetRotation(rotation);
    }
    private void SetVelocity()
    {
       
        {
            _rigidbody.velocity = transform.up * _speed;
        }
    }
}
