using UnityEngine;

public class Enemy : BaseSpawn
{
    
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathAudio;

    private void Update() 
    {
        CalculateBounds();
    }

    private void OnTriggerEnter(Collider other) 
    {       

        if(other.gameObject.tag == "Player")
        {
            PlayerController _player = other.gameObject.GetComponent<PlayerController>();
            _player.PlayerTakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void DestoryEnemy()
    {
        AudioSource.PlayClipAtPoint(deathAudio, transform.position);
        Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);
        Destroy(gameObject);
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