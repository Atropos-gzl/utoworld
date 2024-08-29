using Mesa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32401 : EquipmentFunc
{

    // Start is called before the first frame update
    protected override void Start()
    {
       base.Start();
        gb = GameBase.getInstance();
        buff.attack_perentage -= 0.1f;
        bullet.OnHitEvent += OnHit;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnHit(Bullets bt, GameObject go)
    {
        if (gb.Buy(20))
        {
            float[] damage = new float[2];
            damage[0] = 0;
            damage[1] = 100;
            go.GetComponent<Enemy>().TakeDamage(damage);
            gb.ChangeMoney(-20);
        }
        
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        bullet.OnHitEvent -= OnHit;
    }
}
