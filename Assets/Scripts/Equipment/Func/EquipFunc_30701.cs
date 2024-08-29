using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_30701 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        count = 1;
        max_count = 15;
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        turret.OnCritAttackEvent += OnCritAttack;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {
        if (count <= max_count) {
            TurretBuff buff = gameObject.GetComponent<TurretBuff>();
            buff.crit_dmg += 0.02f;
            value += 0.02f;
            count++;
        }
    }

    protected override void OnDestroy()
    {
        TurretBuff buff = gameObject.GetComponent<TurretBuff>();
        buff.crit_dmg -= value;
        turret.OnCritAttackEvent -= OnCritAttack;
    }
}
