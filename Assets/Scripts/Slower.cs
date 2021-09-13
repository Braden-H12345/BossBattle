using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] float _speedDecrease = 1f;
    protected override void PlayerImpact(Player player)
    {
        TankController controller = player.GetComponent<TankController>();

        if (controller != null)
        {
            controller.MaxSpeed -= _speedDecrease;
        }
    }
    
    
}
