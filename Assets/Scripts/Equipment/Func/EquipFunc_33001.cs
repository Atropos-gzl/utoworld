using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_33001 : EquipmentFunc
{
    public bool done = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        gb = GameBase.getInstance();
        buff = GetComponent<TurretBuff>();
        buff.attack_perentage -= 0.1f;
        buff.magic_attack_percentage -= 0.1f;
        buff.crit_dmg -= 0.1f;
        buff.crit_rate -= 0.1f;
        buff.attack_speed -= 0.1f;
        buff.attack_range_percentage -= 0.1f;
        gb.ChangeMoney((int)(gb.Money * 0.5f));
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!done && gb.Money > 5000)
        {
            done = true;
            buff.attack_perentage += 1.0f;
            buff.magic_attack_percentage += 1.0f;
            buff.crit_dmg += 1.0f;
            buff.crit_rate += 1.0f;
            buff.attack_speed += 1.0f;
            buff.attack_range_percentage += 1.0f;
        }
        else if(done && gb.Money <= 5000)
        {
            done = false;
            buff.attack_perentage -= 1.0f;
            buff.magic_attack_percentage -= 1.0f;
            buff.crit_dmg -= 1.0f;
            buff.crit_rate -= 1.0f;
            buff.attack_speed -= 1.0f;
            buff.attack_range_percentage -= 1.0f;
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        buff.magic_attack_percentage += 0.1f;
        buff.crit_dmg += 0.1f;
        buff.crit_rate += 0.1f;
        buff.attack_speed += 0.1f;
        buff.attack_range_percentage += 0.1f;
        if (done) {
            buff.attack_perentage -= 1.0f;
            buff.magic_attack_percentage -= 1.0f;
            buff.crit_dmg -= 1.0f;
            buff.crit_rate -= 1.0f;
            buff.attack_speed -= 1.0f;
            buff.attack_range_percentage -= 1.0f;
        }
    }
}
