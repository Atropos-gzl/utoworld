using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32101 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        turret.OnEnemyEnterEvent += OnEnemyEnter;
        buff.crit_rate -= 0.05f;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnEnemyEnter(GameObject go)
    {
        float time = Mathf.Max(10, 4 + (int)(gb.Money / 100) * 0.1f);
        EnemyBuff_32101 eb = go.GetComponent<EnemyBuff_32101>();
        if (eb == null) { 
            eb = gameObject.AddComponent<EnemyBuff_32101>();
            eb.enabled = true;
        }
        eb.timer = time;
    }

    protected override void OnDestroy()
    {
        turret.OnEnemyEnterEvent -= OnEnemyEnter;
        buff.crit_rate += 0.05f;
    }
}
