using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    // represents the end of the turret on the tank... where the projectile is instantiated
    [SerializeField] GameObject _endOfTurret;

    [SerializeField] Rigidbody _projectile;

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
            Debug.Log("Semi-Auto Mode");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _shootingMode = 2;
            Debug.Log("Burst Fire");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
            Debug.Log("Bang!!!!");
        }
    }

    private void Fire()
    {
        //todo: visuals and sounds
        if(_shootingMode == 1)
        {
            Rigidbody clone;
            clone = Instantiate(_projectile, _endOfTurret.transform.position, _endOfTurret.transform.rotation);


            clone.velocity = _endOfTurret.transform.forward * 10;
        }

        if(_shootingMode == 2)
        {
            if (_allowNextBurst)
            {
                StartCoroutine(Burst());
            }
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
            clone.velocity = _endOfTurret.transform.forward * 10;
            yield return new WaitForSecondsRealtime(_bulletWaitTime);
        }
        yield return new WaitForSeconds(.25f);
        _allowNextBurst = true;

    }
}
