using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31601 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buff.crit_dmg -= 0.1f;
        turret.OnCritAttackEvent += OnCritAttack;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {
        int rdnum = Random.Range(1, 100);
        if (rdnum <= 30) {
            bullet.GetComponent<Bullets>().damage[2] += 1000;
        }
       
    }

    protected override void OnDestroy()
    {
        turret.OnCritAttackEvent -= OnCritAttack;
        buff.crit_dmg += 0.1f;
    }
}
