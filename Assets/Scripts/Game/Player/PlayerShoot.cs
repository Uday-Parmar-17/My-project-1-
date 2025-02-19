using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private bool _fireSingle;
    [SerializeField]
    private float _timeBetweenShots;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    private bool _fireContinously;
    private float _lastFireTime;
    

    // Update is called once per frame
    void Update()
    {
        if (_fireContinously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }
    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>(); ;

        rigidbody.velocity = _bulletSpeed * transform.up;
    }
    private void OnFire(InputValue inputValue)
    {
        _fireContinously = inputValue.isPressed;
        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
