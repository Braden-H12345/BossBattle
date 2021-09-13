using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : CollectableBase
{

    [SerializeField] float _speedAmount = 5;

    protected override void Collect(Player player)
    {
        TankController controller = player.GetComponent<TankController>();

        if (controller != null)
        {
            controller.MaxSpeed += _speedAmount;
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            (-MovementSpeed, MovementSpeed, -MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
