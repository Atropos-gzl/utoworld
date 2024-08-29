using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31801 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        buff = GetComponent<TurretBuff>();
        buff.attack_perentage -= 0.1f;
        bullet.OnHitEvent += OnHit;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        buff.attack_perentage += 0.1f;
        bullet.OnHitEvent -= OnHit;
    }

    protected override void OnHit(Bullets bt, GameObject go)
    {

        if (bt.isCrit) { 
            if (Random.Range(1,100) <= 30)
            {
                Collider[] enemies = Physics.OverlapSphere(go.transform.position, 4.0f, (1 << 9));
                foreach (Collider collider in enemies) { 
                    if (collider.tag == "Enemy")
                    {
                        float[] damage = new float[2];
                        damage[0] = 0;
                        damage[1] = 100;
                        collider.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
        }


    }
}
