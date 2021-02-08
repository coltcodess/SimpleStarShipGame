using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip fireAudio;

    private void OnEnable() 
    {
        AudioSource.PlayClipAtPoint(fireAudio, transform.position);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > 80)
        {
            ProjectilePooler.Instance.ReturnToPool(this);
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().DestoryEnemy();
            ProjectilePooler.Instance.ReturnToPool(this);
        }    
    }
    
}