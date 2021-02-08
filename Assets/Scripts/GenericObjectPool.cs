using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{

    [SerializeField]
    private T m_prefab;
    
    private Queue<T> objects = new Queue<T>();

    public static GenericObjectPool<T> Instance {get; private set;}

    private void Awake() 
    {
        Instance = this;    
    }

    public T Get(int amount)
    {
        if (objects.Count == 0)
        {
            AddObject(amount);
        }
        
        return objects.Dequeue();        
    }

    private void AddObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObj = GameObject.Instantiate(m_prefab);
            newObj.gameObject.SetActive(true);
            
            objects.Enqueue(newObj);
        }
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }
}
