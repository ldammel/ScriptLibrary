using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance;

    [SerializeField] private GameObject prefab;
    private readonly Queue<GameObject> _objects = new Queue<GameObject>();
    
    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There can never be more than one Pool in a Scene!");
        }
        Instance = this;
    }

    //Add a base amount of Objects to the pool OnEnable
    private void OnEnable()
    {
        AddObject(10);
    }

    ///<summary>Returns a queued object from the pool.</summary>
    public GameObject Get()
    {
        if (_objects.Count == 0)
        {
            //If there are no objects in the Queue, add a new one
            AddObject(1);
        }
        return _objects.Dequeue();
    }

    ///<summary>Instantiates a set amount of new objects and adds them to the queue.</summary>
    private void AddObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab, gameObject.transform, true);
            newObject.SetActive(false);
            _objects.Enqueue(newObject);
            newObject.GetComponent<IGameObjectPooled>().Pool = this;
        }
    }
    ///<summary>Returns the selected object to the queue.</summary>
    public void ReturnToPool(GameObject returnObject)
    {
        returnObject.gameObject.SetActive(false);
        _objects.Enqueue(returnObject);
    }
}