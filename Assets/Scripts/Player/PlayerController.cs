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
    
    Rigidbody _rb;
    Collider _collider;    
    
    private float m_xRange = 4f;
    private float m_horizontalInput;
    private float m_tiltAngle = 5f;
    private float m_firingRate = 0.3f;
    private float m_immuneTimer;
    

    public float timeBetweenFire;    
    public float speed = 10f;
    public bool isDead = false;
    public bool enableMovement = true;   
    public bool isImmune = false;    

    private void OnEnable() 
    {
        EventBroker.LevelComplete += DisableCollider;    
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        
        timeBetweenFire = m_firingRate;
        enableMovement = true;
    }

    // Update is called once per frame
    void Update()
    {     
        if(enableMovement)
        {
            HandleTranlationInput();
            HandleRotation();
            HandleFiringInput();
        }      

        CalculateBounds();
        CalculatePlayerDeath();
    }

    private void HandleRotation()
    {
        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, m_horizontalInput * m_tiltAngle);
    }

    private void HandleTranlationInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * m_horizontalInput * Time.deltaTime * speed;
    }

    private void HandleFiringInput()
    {
        timeBetweenFire += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_firingRate <= timeBetweenFire)
            {
                Fire();
            }
        }
    }

    private void CalculateBounds()
    {
        if (transform.position.x > m_xRange)
        {
            transform.position = new Vector3(m_xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -m_xRange)
        {
            transform.position = new Vector3(-m_xRange, transform.position.y, transform.position.z);
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
            EventBroker.CallGameOver();
            GameTimer.ResetTimer();     
        }
    }

    public void PlayerDeath()
    {
        isDead = true;
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
        enableMovement = false;
    }

    private void OnDisable() 
    {        
        EventBroker.LevelComplete -= DisableCollider;
    }
    
}
