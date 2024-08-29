using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32701 : EquipmentFunc
{
    public bool done = false;
    public GameBase gb;
    // Start is called before the first frame update
    protected override void Start()
    {
        buff = GetComponent<TurretBuff>();
        gb = GameBase.getInstance();
        buff.attack_perentage -= 0.1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!done && gb.Money > 2000)
        {
            done = true;
            buff.crit_dmg += 0.3f;
            buff.crit_rate += 0.2f;
        }
        else if (done && gb.Money < 2000) {
            done = false;
            buff.crit_dmg -= 0.3f;
            buff.crit_rate -= 0.2f;
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        if (done) {
            buff.crit_dmg -= 0.3f;
            buff.crit_rate -= 0.2f;
        }
    }
}
