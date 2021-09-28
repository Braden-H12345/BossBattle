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
    private Rigidbody _rb;
    private bool _phaseTwo = false;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GetComponent<Health>().PhaseTwo == true)
        {
            _phaseTwo = true;
        }
        //transform.LookAt(player.transform);
        _agent.SetDestination(player.transform.position);

        RaycastHit hit;
        if(Physics.Linecast(transform.position, player.transform.position, out hit, _layerIgnore))
        {
            
            
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
        float tempAccel = _agent.acceleration;
        float tempSpeed = _agent.speed;
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


        if(_phaseTwo == false)
        {
            _rb.velocity = transform.forward * 30f;
            yield return new WaitForSeconds(.1f);
        }

        if(_phaseTwo == true)
        {
            _rb.velocity = transform.forward * 60f;
            yield return new WaitForSeconds(.1f);
        }


        _rb.velocity = transform.forward * 0f;

        _agent.acceleration = tempAccel;
        _agent.speed = tempSpeed;
        yield return new WaitForSeconds(5f);
        isDashing = false;
    }
}
