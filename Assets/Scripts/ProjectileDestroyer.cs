using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    [SerializeField] bool _isEnemyProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO: visuals and sounds
        IDamageable test = collision.collider.gameObject.GetComponent<IDamageable>();
        Player player = collision.gameObject.GetComponent<Player>();


        //if it hits player and is an enemy projectile
        if (player != null && _isEnemyProjectile)
        {
            Debug.Log("Hi!");
            IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
            if (playerDmg != null)
            {
                playerDmg.takeDamage(100);
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
                test.takeDamage(100);
            }
        }

        Destroy(this.gameObject);
    }
}
