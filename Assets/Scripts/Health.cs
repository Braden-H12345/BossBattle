using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth;
    [SerializeField] ParticleSystem _damageParticles;
    [SerializeField] AudioClip _damageSound;
    [SerializeField] ParticleSystem _killParticles;
    [SerializeField] AudioClip _killSound;


    private int _currentHealth;

    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void takeDamage(int damage)
    {
        _currentHealth -= damage;
        Feedback();
        if (_currentHealth <= 0)
        {
            KillFeedback();
            Kill();
        }
    }

    void KillFeedback()
    {
        if (_killParticles != null)
        {
            _killParticles = Instantiate(_killParticles, this.transform.position, Quaternion.identity);
            _killParticles.Play();

        }

        if (_killSound != null)
        {
            AudioHelper.PlayClip2D(_killSound, 1f);
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    void Feedback()
    {
        if (_damageParticles != null)
        {
            _damageParticles = Instantiate(_damageParticles, this.transform.position, Quaternion.identity);
            _damageParticles.Play();

        }

        if (_damageSound != null)
        {
            AudioHelper.PlayClip2D(_damageSound, 1f);
        }
    }
}
