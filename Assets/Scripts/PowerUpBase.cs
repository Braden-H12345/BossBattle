using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    [SerializeField] float powerupDuration = 5.0f;
    [SerializeField] Transform visualsToDisable;
    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;


    private Player forPowerDown;
    private float timeElapsed = 0f;
    private bool entered = false;


    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Feedback();
            forPowerDown = player;
            entered = true;
            visualsToDisable.gameObject.SetActive(false);
            BoxCollider c = this.gameObject.GetComponent<BoxCollider>();
            c.enabled = false;
            PowerUp(player);
        }
    }

    private void Feedback()
    {
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }

        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }

    protected virtual void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler(-1, 0, 1);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }


    private void Update()
    {
        if(entered == true)
        {
            
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
            if (timeElapsed >= powerupDuration)
            {
                entered = false;
                PowerDown(forPowerDown);
                Destroy(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }
}
