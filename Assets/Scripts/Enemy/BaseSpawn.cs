using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn : MonoBehaviour
{
    
    [SerializeField] internal float speed = 5f;
    [SerializeField] internal float zBounds = -14f;

        
    public abstract void CalculateBounds();
    
    
    
}
