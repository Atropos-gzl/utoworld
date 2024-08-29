using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31901 : EquipmentFunc
{
    public float cd;
    public float timer;
    public bool instate;
    public bool done;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        cd = 12;
        timer = 8;
        instate = false;
        done = false;
        buff.attack_speed -= 0.1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (cd > 0 && !instate)
        {
            cd -= Time.deltaTime;
        }
        else if(cd <= 0 && !instate)
        {
            cd = 0;
            instate = true;
        }


        if (instate && timer > 0)
        {
            timer -= Time.deltaTime;
            if (!done)
            {
                done = true;
                buff.attack_perentage += 0.1f;
                buff.magic_attack_percentage += 0.1f;
                buff.attack_range_percentage += 0.1f;
                buff.attack_speed += 0.1f;
                buff.crit_dmg += 0.1f;
                buff.crit_rate += 0.1f;
            }
        }
        else if(instate && timer <= 0)
        {
            timer = 0;
            cd = 12;
            instate = false;
            if (done)
            {
                done = false;
                buff.attack_perentage -= 0.1f;
                buff.magic_attack_percentage -= 0.1f;
                buff.attack_range_percentage -= 0.1f;
                buff.attack_speed -= 0.1f;
                buff.crit_dmg -= 0.1f;
                buff.crit_rate -= 0.1f;
            }
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_speed += 0.1f;
        if (done)
        {
            buff.attack_perentage -= 0.1f;
            buff.magic_attack_percentage -= 0.1f;
            buff.attack_range_percentage -= 0.1f;
            buff.attack_speed -= 0.1f;
            buff.crit_dmg -= 0.1f;
            buff.crit_rate -= 0.1f;
        }
    }
}
