using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32601 : EquipmentFunc
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
            if (rdnum <= 10)
            {
                EnemyBuff_32601 eb = go.GetComponent<EnemyBuff_32601>();
                if (eb == null)
                {
                    eb = go.AddComponent<EnemyBuff_32601>();
                    eb.enabled = true;
                }
                eb.timer = 3;
            }
        }
    }

    protected override void OnDestroy() { 
        bullet.OnHitEvent -= OnHit;
        buff.attack_perentage += 0.1f;
    }
}
