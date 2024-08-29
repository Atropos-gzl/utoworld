using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31401 : EquipmentFunc
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        count = 1;
        max_count = 10;
        buff.crit_dmg -= 0.1f;
        gb.onMoneyIncreaseEvent += OnMoneyIncrease;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnMoneyIncrease()
    {
        if (count <= max_count) { 
            int rdnum = Random.Range(0, 100);
            if (rdnum > 50) {
                count++;
                buff.attack_speed += 0.05f;
                value += 0.05f;
            }
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_speed -= value;
        buff.crit_dmg += 0.1f;
        gb.onMoneyIncreaseEvent -= OnMoneyIncrease;
    }
}
