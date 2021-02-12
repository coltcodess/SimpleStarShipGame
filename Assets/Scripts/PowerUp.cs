using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : BaseSpawn
{      

    private void Update()
    {
        CalculateBounds();      

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController _player = other.gameObject.GetComponent<PlayerController>();
            _player.IncreasePowerUp(1);
            Destroy(gameObject);
        }
    }

    public override void CalculateBounds()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z < zBounds)
        {
            Destroy(gameObject);
        }
    }
}
