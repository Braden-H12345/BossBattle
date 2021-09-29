using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

    // represents the end of the turret on the tank... where the projectile is instantiated
    [SerializeField] GameObject _endOfTurret;

    [SerializeField] Rigidbody _projectile;
    [SerializeField] Rigidbody _bigProjectile;

    [SerializeField] ParticleSystem _shootingParticles;
    [SerializeField] AudioClip _shootingSound;
    [SerializeField] Transform _particlePosition;
    [SerializeField] Text _shotMode;
    bool _allowNextBig = true;
    bool _allowNextBurst = true;
    //will have different modes of shooting for now semi auto and burst fire. might add more such as automatic or a shotgun-type of blast.
    private int _shootingMode;
    // Start is called before the first frame update
    void Start()
    {
        _shootingMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _shootingMode = 1;
            _shotMode.text = "Firing Mode: Semi-Auto";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _shootingMode = 2;
            _shotMode.text = "Firing Mode: Burst";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _shootingMode = 3;
            _shotMode.text = "Firing Mode: The Big One";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (_shootingMode == 1)
        {
            Feedback();

            Rigidbody clone;
            clone = Instantiate(_projectile, _endOfTurret.transform.position, _endOfTurret.transform.rotation);


            clone.velocity = _endOfTurret.transform.forward * 20;
        }

        if (_shootingMode == 2)
        {
            if (_allowNextBurst)
            {
                Feedback();
                StartCoroutine(Burst());
            }
        }

        if (_shootingMode == 3)
        {
            if (_allowNextBig)
            {
                Feedback();
                StartCoroutine(BigOne());
            }
        }
    }

    void Feedback()
    {
        if (_shootingParticles != null)
        {
            _shootingParticles = Instantiate(_shootingParticles, _particlePosition.position, Quaternion.identity);
            _shootingParticles.Play();
        }

        if (_shootingSound != null)
        {
            AudioHelper.PlayClip2D(_shootingSound, 1f);
        }
    }

    IEnumerator Burst()
    {
        _allowNextBurst = false;
        float _bulletWaitTime = .15f;
        for( int i=0; i < 3; i++)
        {
            Rigidbody clone;
            clone = Instantiate(_projectile, _endOfTurret.transform.position, _endOfTurret.transform.rotation);
            clone.velocity = _endOfTurret.transform.forward * 20;
            yield return new WaitForSecondsRealtime(_bulletWaitTime);
        }
        yield return new WaitForSeconds(.25f);
        _allowNextBurst = true;

    }

    IEnumerator BigOne()
    {
        _allowNextBig = false;
        Rigidbody clone;
        clone = Instantiate(_bigProjectile, _endOfTurret.transform.position, _endOfTurret.transform.rotation);
        clone.velocity = _endOfTurret.transform.forward * 9;
        yield return new WaitForSeconds(30f);
        _allowNextBig = true;
    }
}
