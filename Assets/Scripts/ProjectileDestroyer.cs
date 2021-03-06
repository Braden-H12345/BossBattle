using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    [SerializeField] bool _isEnemyProjectile;
    [SerializeField] int _damage;
    [SerializeField] ParticleSystem _collisionParticles;
    [SerializeField] AudioClip _collisionSound;
    [SerializeField] float airTime = 12f;

    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(elapsedTime >= airTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Feedback();
        IDamageable test = collision.collider.gameObject.GetComponent<IDamageable>();
        Player player = collision.gameObject.GetComponent<Player>();
        BossMovement boss = collision.gameObject.GetComponent<BossMovement>();

        //if it hits player and is an enemy projectile
        if (player != null && _isEnemyProjectile)
        {
            Debug.Log("Hi!");
            IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
            if (playerDmg != null)
            {
                playerDmg.takeDamage(_damage);
            }
        }

        //if it hits player and is not an enemy projectile
        if (player != null && _isEnemyProjectile == false)
        {
            //do nothing
        }

        //if it does not hit player and is not an enemy projectile
        if(player == null && _isEnemyProjectile == false)
        {
            if (test != null)
            {
                test.takeDamage(_damage);
            }
        }


        Destroy(this.gameObject);
    }

    void Feedback()
    {
        if (_collisionParticles != null)
        {
            _collisionParticles = Instantiate(_collisionParticles, this.transform.position, Quaternion.identity);
            _collisionParticles.Play();

        }

        if (_collisionSound != null)
        {
            AudioHelper.PlayClip2D(_collisionSound, 1f);
        }
    }

}
