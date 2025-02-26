using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncaBotao : MonoBehaviour
{
    Vector3 _direction;

    [SerializeField] float _speed;

    [SerializeField] int _life;
    int _currentLife;

    private void Start()
    {
        _currentLife = _life;
        SetDirection(transform.right);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        if(_currentLife > 0)
            transform.position += _direction * _speed * Time.deltaTime;
    }

    public void TakeDamage(out bool isProtected)
    {
        isProtected = false;
        _currentLife--;

        if (_currentLife <= 0)
            isProtected = true;
    }
}
