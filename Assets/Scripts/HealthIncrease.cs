using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectableBase
{
    // Start is called before the first frame update

    [SerializeField] int _healthAdded = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseHealth(_healthAdded);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
