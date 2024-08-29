using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32501 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        buff = GetComponent<TurretBuff>();
        buff.attack_perentage -= 0.1f;
        buff.crit_dmg += 0.35f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        buff.crit_dmg -= 0.35f;
    }
}
