using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaAttack : MonoBehaviour
{
    //this is to stop the boss when this attack happens, as it is meant to be very powerful. Stops movement dashing and shooting.
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform _areaAttackSpot;
    [SerializeField] Transform _areaAttackPhaseTwo;
    [SerializeField] float _attackRadius;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] GameObject _sphereVisuals;
    [SerializeField] AudioClip _attackSounds;

    Coroutine _currentArea;
    private Rigidbody _rb;
    private Health _health;
    private int _maxHealth;
    private int _currentHealth;
    //first iteration of this attack happens at 85% health
    private float _healthThreshold = .85f;
    private float _timeElapsed = 0f;
    private bool _attackReady = true;
    // Start is called before the first frame update

    private bool _phaseTwo = false;
    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_health.PhaseTwo == true)
        {
            _phaseTwo = true;
            _healthThreshold = .9f;
        }
        //every x% health it will trigger this attack
        //radius will get larger too so it scales difficulty
        _currentHealth = _health.CurrentHealth;
        _maxHealth = _health.MaxHealth;

        if(((float)_currentHealth / _maxHealth) <= _healthThreshold)
        {
            if (_attackReady)
            {
                if(_currentArea != null)
                {
                    StopCoroutine(_currentArea);
                }

                _currentArea = StartCoroutine(StartAreaAttack());
                _healthThreshold = (float)_currentHealth;
                _healthThreshold -= .1f;
            }
        }

    }

    IEnumerator StartAreaAttack()
    {
        _rb.velocity = transform.forward * 0;
        _attackReady = false;
        GameObject sphere = null;
        GameObject sphere1 = null;
        GameObject sphere2 = null;

        float tempSpeed = _agent.speed;
        //stops movement
        _agent.speed = 0;
        //stops dashing
        BossMovement dash = GetComponent<BossMovement>();
        dash.DashReady = false;

        Vector3 largeRadius;
        largeRadius.x = 16;
        largeRadius.y = 16;
        largeRadius.z = 16;

        yield return new WaitForSeconds(.75f);
        //attacks
        if (_phaseTwo == false)
        {
            for(int i=0; i<5;i++)
            {
                sphere1 = Instantiate(_sphereVisuals, _areaAttackSpot.position, _areaAttackSpot.rotation);
                yield return new WaitForSeconds(.1f);
                Destroy(sphere1);
                yield return new WaitForSeconds(.1f);
            }
            sphere = Instantiate(_sphereVisuals, _areaAttackSpot.position, _areaAttackSpot.rotation);
            Feedback();
            while(_timeElapsed <= 1.5f)
            {
                Collider[] hit = Physics.OverlapSphere(_areaAttackSpot.position, _attackRadius, _enemyLayer);
                foreach (Collider player in hit)
                {
                    if (player != null)
                    {
                        IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
                        playerDmg.takeDamage(5);
                        yield return new WaitForSeconds(.25f);
                    }
                }
                yield return null;
                _timeElapsed += Time.deltaTime;
            }

        }
        else
        {
            for(int i=0; i<5; i++)
            {
                sphere2 = Instantiate(_sphereVisuals, _areaAttackPhaseTwo.position, _areaAttackPhaseTwo.rotation);
                sphere2.transform.localScale = largeRadius;
                yield return new WaitForSeconds(.1f);
                Destroy(sphere2);
                yield return new WaitForSeconds(.1f);
            }
            sphere = Instantiate(_sphereVisuals, _areaAttackPhaseTwo.position, _areaAttackPhaseTwo.rotation);
            sphere.transform.localScale = largeRadius;
            Feedback();
            while (_timeElapsed <= 3f)
            {
                Collider[] hit = Physics.OverlapSphere(_areaAttackPhaseTwo.position, 8f, _enemyLayer);
                foreach (Collider player in hit)
                {
                    if (player != null)
                    {
                        IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
                        playerDmg.takeDamage(5);
                        yield return new WaitForSeconds(.1f);
                    }
                }
                yield return null;
                _timeElapsed += Time.deltaTime;
            }
        }

        Destroy(sphere);
        _timeElapsed = 0f;
        //start moving
        _agent.speed = tempSpeed;
        //start dashing
        dash.DashReady = true;

        yield return new WaitForSeconds(4f);
        _attackReady = true;
    }

    private void Feedback()
    {
        if (_attackSounds != null)
        {
            AudioHelper.PlayClip2D(_attackSounds, 1f);
        }
    }
}
