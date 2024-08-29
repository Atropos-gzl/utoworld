using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32001 : EquipmentFunc
{
    public bool done;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buff.attack_speed -= 0.1f;
        done = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (buff.attack_speed < 0)
        {
            int num = (int)((-buff.attack_speed) / 0.1f);
            if (done)
            {
                buff.attack_perentage -= value;
                value = Mathf.Min(10, 0.3f * num);
                buff.attack_perentage += value;
            }
            else
            {
                value = Mathf.Min(10, 0.3f * num);
                buff.attack_perentage += value;
                done = true;
            }

        }
    }

    protected override void OnDestroy()
    {
        buff.attack_speed += 0.1f;
        if (done) {
            buff.attack_perentage -= value;
        }
    }
}
