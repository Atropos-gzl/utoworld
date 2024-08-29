using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31501 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        buff = GetComponent<TurretBuff>();
        buff.crit_rate -= 0.05f;
        gb = GameBase.getInstance();
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        bullet.OnHitEvent += OnHit;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnHit(Bullets bt, GameObject go)
    {
        float time = (int)(gb.Money / 100) * 0.1f;
        time = Mathf.Max(time + 3, 8);
        EnemyBuff eb = go.GetComponent<EnemyBuff_31501>();
        if (eb == null)
        {
            eb = go.AddComponent<EnemyBuff_31501>();
            eb.enabled = true;
        }
        eb.timer = time;
        
        
    }

    protected override void OnDestroy()
    {
        bullet.OnHitEvent -= OnHit;
        buff.crit_rate += 0.05f;
    }


}
