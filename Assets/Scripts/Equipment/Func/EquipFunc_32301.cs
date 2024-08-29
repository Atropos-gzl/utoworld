using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_32301 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        buff.attack_speed -= 0.1f;
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
            Collider[] enemies = Physics.OverlapSphere(go.transform.position, 6.0f, (1 << 9));
            foreach (Collider collider in enemies)
            {
                if (collider.tag == "Enemy")
                {
                    EnemyBuff_32301 eb = collider.GetComponent<EnemyBuff_32301>();
                    if (eb == null)
                    {
                        eb = collider.gameObject.AddComponent<EnemyBuff_32301>();
                        eb.enabled = true;
                    }
                    eb.timer = 3;
                }
            }
        }


    }

    protected override void OnDestroy()
    {
        buff.attack_speed += 0.1f;
        bullet.OnHitEvent -= OnHit;
    }
}
