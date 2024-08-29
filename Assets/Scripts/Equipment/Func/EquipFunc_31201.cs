using Mesa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_31201 : EquipmentFunc
{
    public List<GameObject> summons;
    // Start is called before the first frame update
    protected override void Start()
    {
        buff = GetComponent<TurretBuff>();
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        creature_name = "20301";
        summons = new List<GameObject>();
        bullet.OnKilledEvent += KillEnemy;
        buff.crit_rate -= 0.05f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void KillEnemy(Turret turret, GameBase gamebase)
    {
        GameObject go = Instantiate(GameResourceManager.getPrefabs(creature_name), transform.position, Quaternion.identity, transform);
        summons.Add(go);
    }

    protected override void OnDestroy() {
        buff.crit_rate += 0.05f;
        foreach (GameObject go in summons) {
            if (go != null) {
                go.GetComponent<Summon>().Dead();
            }
        }
        bullet.OnKilledEvent -= KillEnemy;
    }


}
