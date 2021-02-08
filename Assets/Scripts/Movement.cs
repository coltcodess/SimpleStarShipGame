using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    bool b_canMove = true;

    private void OnEnable() 
    {
        EventBroker.GameOver += StopMovement;
        EventBroker.LevelComplete += StopMovement;      
    }

    // Update is called once per frame
    void Update()
    {
        if(b_canMove)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }        
    }

    public void StopMovement()
    {
        b_canMove = false;
    }

    private void OnDisable() 
    {
        EventBroker.GameOver -= StopMovement;
        EventBroker.LevelComplete -= StopMovement;
    }




}
