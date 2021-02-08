using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    bool canMove = true;

    private void OnEnable() 
    {
        EventBroker.GameEnded += StopMovement;        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }        
    }

    public void StopMovement()
    {
        canMove = false;
    }

    private void OnDisable() 
    {
        EventBroker.GameEnded -= StopMovement;
        
    }




}
