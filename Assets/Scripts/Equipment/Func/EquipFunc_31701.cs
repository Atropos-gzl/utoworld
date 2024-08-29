using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31701 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buff.attack_perentage -= 0.1f;
        bullet.OnHitEvent += OnHit;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnHit(Bullets bt, GameObject go)
    {
        if (bt.isCrit)
        {
            int rdnum = Random.Range(1, 100);
            if (rdnum <= 30)
            {
                //go.
            }
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        bullet.OnHitEvent -= OnHit;
    }
}
