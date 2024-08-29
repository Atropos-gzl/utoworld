using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_30401 : EquipmentFunc
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        count = 0;
        total_count = 0;
        max_count = 10;
        attack_interval = 1;
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
        if (total_count <= max_count && count == 1) { 
            buff.attack_speed += 0.02f;
            value += 0.02f;
        }
        count %= attack_interval;
    }

    protected override void OnDestroy()
    {
        buff.attack_speed -= value;
        turret.OnIncreasePowerEvent -= IncreasePower;
    }
}
