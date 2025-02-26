using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class POOButton : MonoBehaviour
{
    protected Vector3 _direction;
    [SerializeField] protected float _minSpeed, _maxSpeed;
    protected float _speed;


    [SerializeField] protected float minPosX, maxPosX;
    protected RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    public bool ReturnAbstracrionValue()
    {
        return false;
    }

    public void SetDirection()
    {
        _direction = Random.insideUnitCircle.normalized;
    }

    public void Move()
    {
        _rectTransform.position += _direction * _speed * Time.deltaTime * 10;


        if (_rectTransform.anchoredPosition.y > 60 || _rectTransform.anchoredPosition.y < -345)
        {
            _direction.y *= -1;
        }

        if (_rectTransform.anchoredPosition.x > maxPosX || _rectTransform.anchoredPosition.x < minPosX)
        {
            _direction.x *= -1;
        }
    }
}
