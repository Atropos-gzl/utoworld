using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32801 : EquipmentFunc
{
    public float cd;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        cd = 10;
        buff.attack_perentage -= 0.1f;
        bullet.OnHitEvent += OnHit;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (cd > 0)
        {
            cd -= Time.deltaTime;
        }
        else {
            cd = 0;
        }
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        bullet.OnHitEvent -= OnHit;
    }

    protected override void OnHit(Bullets bt, GameObject go)
    {
        EnemyBuff_32801 eb = go.GetComponent<EnemyBuff_32801>();
        if ( eb == null && cd == 0 )
        {
            eb = go.AddComponent<EnemyBuff_32801>();
            eb.enabled = true;
            cd = 10;
        }
    }
}
