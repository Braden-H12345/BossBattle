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
                StartCoroutine(StartAreaAttack());
                _healthThreshold -= .1f;
            }
        }

    }

    IEnumerator StartAreaAttack()
    {
        _attackReady = false;
        GameObject sphere = null;
        float tempSpeed = _agent.speed;
        //stops movement
        _agent.speed = 0;
        //stops shooting
        BossShooting shoot = GetComponent<BossShooting>();
        shoot.ShotReady = false;

        Vector3 largeRadius;
        largeRadius.x = 14;
        largeRadius.y = 14;
        largeRadius.z = 14;

        yield return new WaitForSeconds(.75f);
        //attacks
        if (_phaseTwo == false)
        {
            sphere = Instantiate(_sphereVisuals, _areaAttackSpot.position, _areaAttackSpot.rotation);

            yield return new WaitForSeconds(.5f);
            Collider[] hit = Physics.OverlapSphere(_areaAttackSpot.position, _attackRadius, _enemyLayer);
            foreach (Collider player in hit)
            {
                Player playerObject = player.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
                    playerDmg.takeDamage(100);
                }
            }
        }
        else
            {
            sphere = Instantiate(_sphereVisuals, _areaAttackPhaseTwo.position, _areaAttackPhaseTwo.rotation);
            sphere.transform.localScale = largeRadius;
            yield return new WaitForSeconds(.5f);
            Collider[] hit = Physics.OverlapSphere(_areaAttackPhaseTwo.position, 7f, _enemyLayer);
                foreach (Collider player in hit)
                {
                    Player playerObject = player.gameObject.GetComponent<Player>();
                    if (player != null)
                    {
                        IDamageable playerDmg = player.gameObject.GetComponent<IDamageable>();
                        playerDmg.takeDamage(100);
                    }
                }
            }

        Destroy(sphere);
        _timeElapsed = 0f;
        yield return new WaitForSeconds(.75f);
        //start moving
        _agent.speed = tempSpeed;
        //start shooting
        shoot.ShotReady = true;
        yield return new WaitForSeconds(4f);
        _attackReady = true;
    }
}
