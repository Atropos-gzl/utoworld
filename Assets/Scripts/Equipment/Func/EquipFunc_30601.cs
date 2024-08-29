using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EquipFunc_30601 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        bullet.OnKilledEvent += KillEnemy;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void KillEnemy(Turret turret, GameBase gamebase)
    {
        gamebase.ChangeMoney(5);
    }

    protected override void OnDestroy()
    {
        bullet.OnKilledEvent -= KillEnemy;
    }
}
