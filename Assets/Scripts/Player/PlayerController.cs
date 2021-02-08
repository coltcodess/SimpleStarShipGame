using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] Projectile _projectile;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] public int health = 3;
    [SerializeField] public int powerUpAmount = 0;
    [SerializeField] AudioClip deathAudio;

    public delegate void HealthDelegate();
    public event HealthDelegate HealthChangedEvent;

    public delegate void PowerUpDelegate();
    public event PowerUpDelegate PowerUpChangedEvent;


    float horizontalInput;
    Rigidbody _rb;
    Collider _collider;
    public float speed = 10f;
    private float xRange = 4f;
    private float tiltAngle = 5f;
    private float firingRate = 0.3f;
    public float timeBetweenFire;
    public bool isDead;
    
    public bool isImmune;

    private float _immuneTimer;

    private void OnEnable() 
    {
        EventBroker.GameEnded += DisableCollider;    
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        timeBetweenFire = firingRate;
    }

    // Update is called once per frame
    void Update()
    {        

        HandleTranlationInput();
        HandleRotation();
        HandleFiringInput();

        HandleUIInput();

        CalculateBounds();
        CalculatePlayerDeath();
    }

    private void HandleUIInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.isGameActive)
            {
                GameManager.isGameActive = false;
            }
            else
            {
                GameManager.isGameActive = true;
            }
        }
    }

    private void HandleRotation()
    {
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, horizontalInput * tiltAngle);
    }

    private void HandleTranlationInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * horizontalInput * Time.deltaTime * speed;
    }

    private void HandleFiringInput()
    {
        timeBetweenFire += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (firingRate <= timeBetweenFire)
            {
                Fire();
            }
        }
    }

    private void CalculateBounds()
    {
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }

    private void Fire()
    {
        timeBetweenFire = 0f;
        var shot = ProjectilePooler.Instance.Get(1);
        
        shot.transform.position = transform.position;
        shot.gameObject.SetActive(true);
    }

    private void CalculatePlayerDeath()
    {
        if (health == 0 && !isDead)
        {
            PlayerDeath();
            isDead = true;
        }
    }

    public void PlayerDeath()
    {
        Instantiate(deathParticles, transform.position, deathParticles.transform.rotation); 
        AudioSource.PlayClipAtPoint(deathAudio, transform.position);     
        
        Destroy(gameObject);
    }
    

    public void PlayerTakeDamage(int damage)
    {
        if(!isImmune)
        {
            health -= damage;
            if (HealthChangedEvent != null)
            {
                HealthChangedEvent();
            }
            
        }        
    }

    public void IncreasePowerUp(int powerUp)
    {
        if(!isDead)
        {
            powerUpAmount += powerUp;
            if(PowerUpChangedEvent != null)
            {
                PowerUpChangedEvent();
            }
            
        }
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void OnDisable() 
    {
        EventBroker.GameEnded -= DisableCollider;
    }

    
    
}
