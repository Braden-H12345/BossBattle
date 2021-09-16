using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectableBase
{
    // Start is called before the first frame update


    protected override void Collect(Player player)
    {
        //player.IncreaseTreasure();
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            ( MovementSpeed, -MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
