using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31301 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        count = 0;
        max_count = 10;
        buff = GetComponent<TurretBuff>();
        buff.attack_speed -= 0.1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {
        if (count <= max_count) { 
            count++;
            buff.attack_perentage += 0.05f;
            value += 0.05f;
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_speed += 0.1f;
        buff.attack_perentage -= value;
    }
}
