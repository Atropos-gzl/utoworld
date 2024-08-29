using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EquipFunc_30301 : EquipmentFunc
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attack_interval = 5;
        turret.OnIncreasePowerEvent += IncreasePower;

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void IncreasePower(Turret turret, GameObject bullet, GameBase gb)
    {
        count++;
        if (count == attack_interval) {
            gb.ChangeMoney(1);
        }
        count %= attack_interval;
        
    }

    protected override void OnDestroy()
    {
        turret.OnIncreasePowerEvent -= IncreasePower;
    }
}
