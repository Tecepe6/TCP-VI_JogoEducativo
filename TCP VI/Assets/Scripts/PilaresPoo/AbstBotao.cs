using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstBotao : MonoBehaviour
{
    Vector3 _direction;
    [SerializeField] float _lifeTime;
    [SerializeField] float _speed;

    [SerializeField] bool correta;


    public bool ReturnAbstracrionValue()
    {
        return correta;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        _lifeTime -= Time.deltaTime;

        if(_lifeTime < 0)
            Destroy(gameObject);
    }
}
