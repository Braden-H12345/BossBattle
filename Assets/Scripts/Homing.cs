using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    // Start is called before the first frame update]
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;

    private Player _player;
    private Rigidbody _rb;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_player != null)
        {
            Vector3 direction = _player.gameObject.transform.position - _rb.position;
            direction.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
            _rb.angularVelocity = rotationAmount * _rotationForce;
            _rb.velocity = transform.forward * _force;
        }
    }
}
