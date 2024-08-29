using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_30501 : EquipmentFunc
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        count = 0;
        total_count = 0;
        max_count = 10;
        attack_interval = 1;
        value = 0;
        turret.OnIncreasePowerEvent += IncreasePower;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void IncreasePower(Turret turret, GameObject bullet, GameBase gb)
    {
        count++;
        total_count += 1;
        if (total_count <= max_count && count == 1)
        {
            buff.crit_rate += 0.01f;
            value += 0.01f;
        }
        count %= attack_interval;
    }

    protected override void OnDestroy()
    {
        buff.crit_rate -= value;
        turret.OnIncreasePowerEvent -= IncreasePower;
    }
}
