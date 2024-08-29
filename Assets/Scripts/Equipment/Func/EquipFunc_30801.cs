using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFunc_30801 : EquipmentFunc
{
    // Start is called before the first frame update
    protected override void Start()
    {
        turret = GetComponent<Turret>();
        bullet = turret.bullet.GetComponent<Bullets>();
        turret.OnCritAttackEvent += OnCritAttack;
    }

    // Update is called once per frame
    protected override void Update()
    {
    }

    protected override void OnCritAttack(Turret turret, GameObject bullet, GameBase gb)
    {
        gb.ChangeMoney(3);
        count++;
    }

    protected override void OnDestroy()
    {
        turret.OnCritAttackEvent -= OnCritAttack;
    }


}
