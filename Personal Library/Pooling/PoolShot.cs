using UnityEngine;

public class PoolShot : MonoBehaviour
{
    [SerializeField]
    private float fireRate;

    [SerializeField] private Pool objectPool;

    private float _fireTimer;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_fireTimer >= fireRate)
            {
                _fireTimer = 0;
                Fire();
            }
        }

        _fireTimer += Time.deltaTime;
    }

    private void Fire()
    {
        var shot = objectPool.Get();
        var transform1 = transform;
        shot.transform.position = transform1.position;
        shot.transform.rotation = transform1.rotation;
        shot.gameObject.SetActive(true);
    }
}
