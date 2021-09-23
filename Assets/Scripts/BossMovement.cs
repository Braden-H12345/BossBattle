using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    [SerializeField] ParticleSystem dashParticles;
    [SerializeField] AudioClip dashSound;
    [SerializeField] GameObject dashParticlePosition;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] LayerMask _layerIgnore;
    private bool isDashing = false;
    private float timeElapsed = 0f;
    private bool isBlocked = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        _agent.SetDestination(player.transform.position);

        RaycastHit hit;
        if(Physics.Linecast(transform.position, player.transform.position, out hit, _layerIgnore))
        {
            
            Debug.Log(hit.transform.gameObject.name);
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }


        if (!isDashing && timeElapsed >= 6)
        {
            if (!isBlocked)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
    }

    IEnumerator Dash()
    {
        if (dashParticles != null)
        {
            dashParticles = Instantiate(dashParticles, dashParticlePosition.transform.position, Quaternion.identity);
            dashParticles.Play();

        }

        if (dashSound != null)
        {
            AudioHelper.PlayClip2D(dashSound, 1f);
        }

        isDashing = true;

        for(int i=0; i<9;i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .9f);
        }
        yield return new WaitForSeconds(12f);
        isDashing = false;
    }
}
