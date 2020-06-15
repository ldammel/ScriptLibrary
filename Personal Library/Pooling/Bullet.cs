using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IGameObjectPooled
{
    public float moveSpeed = 30f;

    private float _lifeTime;
    public float maxLifeTime;

    private Pool _pool;

    public Pool _Pool
    {
        get => _pool;
        set
        {
            if (_pool == null)
                _pool = value;
            else 
                throw new Exception("Bad pool use, this should only get set once!");
        }
    }

    private void OnEnable()
    {
        _lifeTime = 0f;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector3.forward);
        _lifeTime += Time.deltaTime;
        if (_lifeTime > maxLifeTime) //Return the object to the pool if the time runs out
        {
            _pool.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _pool.ReturnToPool(gameObject);//Return the object to the pool if it collides with an object
    }
}

internal interface IGameObjectPooled
{
    Pool _Pool { get; set; }
}