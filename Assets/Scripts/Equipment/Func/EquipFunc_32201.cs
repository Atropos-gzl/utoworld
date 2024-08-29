using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32201 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Turret turret = GetComponent<Turret>();
        buff = GetComponent<TurretBuff>();
        buff.attack_speed -= 0.1f;
        turret.OnCritAttackEvent += OnCritAttack;

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {
        int num = gb.Money / 100;
        buff.attack_perentage += 0.05f;
        value += 0.05f;
    }

    protected override void OnDestroy()
    {
        buff.attack_speed += 0.1f;
        buff.attack_perentage -= value;
        turret.OnCritAttackEvent -= OnCritAttack;
    }
}
