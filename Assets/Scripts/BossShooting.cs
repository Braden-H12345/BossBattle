using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [SerializeField] Transform _shotPos2;
    [SerializeField] Transform _shotPos1;
    [SerializeField] Rigidbody _projectile;
    [SerializeField] ParticleSystem _shotParticles;
    [SerializeField] AudioClip _shotSound;

    Coroutine _currentShot;
    private int _shotPosChooser;
    private bool _shotReady = true;
    private float _timeElapsed = 0f;
    private Health health;
    // Start is called before the first frame update

    public bool ShotReady
    {
        set => _shotReady = value;
    }
    void Start()
    {
        health = GetComponent<Health>();
        _shotPosChooser = Random.Range((int)1, (int)2);
    }

    // Update is called once per frame
    void Update()
    {
        if(health.PhaseTwo)
        {
            _shotPosChooser = 3;
        }
        if(_shotReady && _timeElapsed >= 3)
        {
            if(_currentShot != null)
            {
                StopCoroutine(_currentShot);
            }
            
            _currentShot = StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        _timeElapsed += Time.deltaTime;
    }

    IEnumerator Shoot()
    {
        _shotReady = false;
        Transform position;

        if(_shotPosChooser == 1)
        {
            Feedback1();
            position = _shotPos1;
            _shotPosChooser = 2;
        }
        else if(_shotPosChooser == 2)
        {
            Feedback2();
            position = _shotPos2;
            _shotPosChooser = 1;
        }
        else
        {
            Feedback3();
            position = _shotPos2;
            Rigidbody clone1;
            clone1 = Instantiate(_projectile, _shotPos1.transform.position, _shotPos1.transform.rotation);


            clone1.velocity = _shotPos1.transform.forward * 30;
        }

        Rigidbody clone;
        clone = Instantiate(_projectile, position.transform.position, position.transform.rotation);


        clone.velocity = position.transform.forward * 30;

        yield return new WaitForSeconds(3f);
        _shotReady = true;
    }

    private void Feedback1()
    {
        if (_shotParticles != null)
        {
            _shotParticles = Instantiate(_shotParticles, _shotPos1.position, Quaternion.identity);
            _shotParticles.Play();
        }

        if (_shotSound != null)
        {
            AudioHelper.PlayClip2D(_shotSound, 1f);
        }
    }

    private void Feedback2()
    {
        if (_shotParticles != null)
        {
            _shotParticles = Instantiate(_shotParticles, _shotPos2.position, Quaternion.identity);
            _shotParticles.Play();
        }

        if (_shotSound != null)
        {
            AudioHelper.PlayClip2D(_shotSound, 1f);
        }
    }

    private void Feedback3()
    {
        if (_shotParticles != null)
        {
            _shotParticles = Instantiate(_shotParticles, _shotPos1.position, Quaternion.identity);
            _shotParticles.Play();

            ParticleSystem _shotParticles2;
            _shotParticles2 = Instantiate(_shotParticles, _shotPos2.position, Quaternion.identity);
            _shotParticles2.Play();
        }

        if (_shotSound != null)
        {
            AudioHelper.PlayClip2D(_shotSound, 1f);
        }
    }
}
