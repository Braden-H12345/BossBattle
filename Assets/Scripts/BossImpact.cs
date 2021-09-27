using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImpact : MonoBehaviour
{
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
        Player player = collision.collider.gameObject.GetComponent<Player>();

        if(player != null)
        {
            IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();

            playerDmg.takeDamage(15);
        }
    }
}
