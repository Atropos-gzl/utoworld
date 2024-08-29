using System;
using System.Collections;
using System.Collections.Generic;
using CSV;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData CrossBow;
    public TurretData FattyPlasma;
    public TurretData Culverin;
    public GameObject Build_Effect;


    private GameBase Base;
    private Transform build_pos;
    private Collider cube_coll;
    private Animator flicker;



    private void Start()
    {
        //flicker = GameObject.Find("Money").GetComponent<Animator>();
        Base = GameObject.Find("GameManager").GetComponent<GameBase>();
        getConfig(Application.dataPath+"/Config/ConfigFile_TowerData.csv");
    }
    public GameObject CreateTurret(TurretData Selection)
    {

        //获取位置，更改Cube信息
        build_pos = GameObject.Find("MouseManager").GetComponent<Mouse>().build_position;
        cube_coll = GameObject.Find("MouseManager").GetComponent<Mouse>().cube_collider;
        //建造
        GameObject go = Instantiate(Selection.turretPrafab[Selection.level], build_pos.position + new Vector3(0, build_pos.localScale.y, 0), Quaternion.identity) as GameObject;
        //赋予塔信息
        switch (Selection.type)
        {
            case TurretType.CrossBow:
                //GetData(go.GetComponent<CrossBow>().Selection, Selection);
                go.GetComponent<CrossBow>().Selection = Selection;
                go.GetComponent<CrossBow>().resetAttrib();
                break;
            case TurretType.FattyPlasma:
                //GetData(go.GetComponent<LaserTurret>().Selection, Selection);
                go.GetComponent<LaserTurret>().Selection = Selection;
                go.GetComponent<LaserTurret>().resetAttrib();
                break;
            case TurretType.Culverin:
                //GetData(go.GetComponent<Turret>().Selection, Selection);
                go.GetComponent<Turret>().Selection = Selection;
                go.GetComponent<Turret>().resetAttrib();
                break;
        }
        go.tag = "turret";
        //动画
        go.GetComponentInChildren<Animator>().SetTrigger("Install");
        //建筑特效
        GameObject effect = Instantiate(Build_Effect, build_pos.position + new Vector3(0, 0.54f, 0), Quaternion.identity);
        Destroy(effect, 1.1f);
        //该方块已有炮塔
        cube_coll.GetComponent<MapCube>().times++;
        cube_coll.GetComponent<MapCube>().used = false;
        //GetData(cube_coll.GetComponent<MapCube>().memory, Selection);
        cube_coll.GetComponent<MapCube>().memory = Selection;
        cube_coll.GetComponent<MapCube>().turret = go;
        Debug.Log(cube_coll.name);
        return go;
    }

    public void UpgradeTurret(Collider collider)
    {
        Debug.Log("upgrade");
        build_pos = GameObject.Find("MouseManager").GetComponent<Mouse>().build_position;
        cube_coll = GameObject.Find("MouseManager").GetComponent<Mouse>().cube_collider;
        TurretData Selection = cube_coll.GetComponent<MapCube>().memory;
        Selection.level++;
        GameObject go = cube_coll.GetComponent<MapCube>().turret;
        GameObject child_turret = go.transform.GetChild(0).gameObject;
        
        GameObject new_turret = null;
        switch (Selection.type)
        {
            case TurretType.CrossBow:
                new_turret = GameResourceManager.getPrefabs(CrossBow.ID[Selection.level].ToString());
                go.GetComponent<CrossBow>().resetAttrib();
                break;
            case TurretType.FattyPlasma:
                new_turret = GameResourceManager.getPrefabs(FattyPlasma.ID[Selection.level].ToString());
                go.GetComponent<LaserTurret>().resetAttrib();
                break;
            case TurretType.Culverin:
                new_turret = GameResourceManager.getPrefabs(Culverin.ID[Selection.level].ToString());
                go.GetComponent<Turret>().resetAttrib();
                break;
        }

        //GameObject turret_instance = Instantiate(new_turret, build_pos.position + new Vector3(0, build_pos.localScale.y, 0), child_turret.transform);
        GameObject turret_instance = Instantiate(new_turret, child_turret.transform.position,child_turret.transform.rotation, go.transform);
        //turret_instance.name = "Turret";
        //AnimatorOverrideController controller = 
        Destroy(child_turret);
        //turret_instance.transform.SetParent(go.transform);
        go.GetComponentInChildren<Animator>().SetTrigger("Install");
        cube_coll.GetComponent<MapCube>().times++;
        cube_coll.GetComponent<MapCube>().used = false;
        //GetData(cube_coll.GetComponent<MapCube>().memory, Selection);
        cube_coll.GetComponent<MapCube>().memory = Selection;

        //EquipmentFunc[] funcs = go.GetComponents<EquipmentFunc>();

        //更新Selectioin

        //建造
        //GameObject upgrade_turret = CreateTurret(Selection);
        //Debug.Log("check " + go.name); 
        //foreach (EquipmentFunc ef in funcs)
        //{
        //    UnityEditorInternal.ComponentUtility.CopyComponent(ef);
        //    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(upgrade_turret);
        //    EquipmentFunc equipmentFunc = upgrade_turret.GetComponent<EquipmentFunc>();
        //    equipmentFunc.enabled = true;
        //    UnityEditorInternal.ComponentUtility.PasteComponentValues(equipmentFunc);
        //}
        //Destroy(go);
        //UnityEditorInternal.ComponentUtility.

    }

    public void RemoveTurret(Collider collider)
    {
        //GameObject.FindGameObjectWithTag("Upgrade_UI").SendMessage("Disappear");
        cube_coll = collider;
        cube_coll.GetComponent<MapCube>().used = true;
        TurretData Selection = cube_coll.GetComponent<MapCube>().memory;
        Destroy(cube_coll.GetComponent<MapCube>().turret);
        Base.ChangeMoney((int)(Selection.cost[Selection.level] * 0.6f));
        GameObject.Find("Money").GetComponent<Text>().text = "$" + Base.Money.ToString();
    }

    public void OnCrossBowSelected()
    {
        //GameObject.FindGameObjectWithTag("Select_UI").SendMessage("Disappear");
        //TurretData Selection = new TurretData();
        //GetData(Selection, CrossBow);
        TurretData Selection = CrossBow;
        Selection.level = 0;

        if (Base.Buy(Selection.cost[Selection.level]))
        {
            CreateTurret(Selection);
            Base.ChangeMoney(-Selection.cost[0]);
        }
        else
        {
            //flicker.SetTrigger("flick");
        }


    }

    public void OnFattyPlasmaSelected()
    {
        //GameObject.FindGameObjectWithTag("Select_UI").SendMessage("Disappear");
        TurretData Selection = FattyPlasma;
        Selection.level = 0;
        if (Base.Buy(Selection.cost[Selection.level]))
        {
            CreateTurret(Selection);
            Base.ChangeMoney(-Selection.cost[0]);
        }
        else
        {
            //flicker.SetTrigger("flick");
        }
    }

    public void OnCulverinSelected()
    {
        //GameObject.FindGameObjectWithTag("Select_UI").SendMessage("Disappear");
        TurretData Selection = Culverin;
        Selection.level = 0;
        if (Base.Buy(Selection.cost[Selection.level]))
        {
            CreateTurret(Selection);
            Base.ChangeMoney(-Selection.cost[0]);
        }
        else
        {
            //flicker.SetTrigger("flick");
        }
    }

    public void OnUpgradeSelected(TurretData turretData,Collider collider)
    {
        TurretData Selection = turretData;
        if (Selection.level == 3) {
            return;
        }
        //GameObject.FindGameObjectWithTag("Upgrade_UI").SendMessage("Disappear");
        if (Base.Buy(Selection.cost[Selection.level]))
        {
            UpgradeTurret(collider);
            Base.ChangeMoney(-Selection.cost[Selection.level]);
        }
        else
        {
           //flicker.SetTrigger("flick");
        }

    }

    private void GetData(TurretData Selection, TurretData Selected)
    {
        Selection.attack = new float[4];
        Selection.cost = new int[4];
        Selection.range = new float[4];
        Selection.rate = new float[4];
        Selection.turretPrafab = new GameObject[4];

        Selected.attack.CopyTo(Selection.attack, 0);
        Selected.cost.CopyTo(Selection.cost, 0);
        Selected.range.CopyTo(Selection.range, 0);
        Selected.rate.CopyTo(Selection.rate, 0);
        Selected.turretPrafab.CopyTo(Selection.turretPrafab, 0);
        Selection.type = Selected.type;
        Selection.level = Selected.level;
        if (Selection.type == TurretType.CrossBow)
        {
            Selection.speed = Selected.speed;
        }
        if (Selection.type != TurretType.FattyPlasma)
        {
            Selection.bullet_Prefab = Selected.bullet_Prefab;
        }
    }

    private void getConfig(string filepath)
    {
        CSV.CSVData data = new CSV.CSVData();
        try
        {
            data.OpenCSV(filepath);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
        CrossBow.range = new float[4];
        CrossBow.cost = new int[4];
        CrossBow.crit_rate = new float[4];
        CrossBow.crit_dmg = new float[4];
        CrossBow.attack = new float[4];
        CrossBow.magic_attack = new float[4];
        CrossBow.rate = new float[4];
        CrossBow.ID = new int[4];
        Culverin.range = new float[4];
        Culverin.cost = new int[4];
        Culverin.crit_rate = new float[4];
        Culverin.crit_dmg = new float[4];
        Culverin.attack = new float[4];
        Culverin.magic_attack = new float[4];
        Culverin.rate = new float[4];
        Culverin.ID = new int[4];
        FattyPlasma.range = new float[4];
        FattyPlasma.cost = new int[4];
        FattyPlasma.crit_rate = new float[4];
        FattyPlasma.crit_dmg = new float[4];
        FattyPlasma.attack = new float[4];
        FattyPlasma.magic_attack = new float[4];
        FattyPlasma.rate = new float[4];
        FattyPlasma.ID = new int[4];
        for (int i = 0; i < 4; i++)
        {
            try
            {
                //CrossBow.range[i] = Convert.ToSingle(data.get(i, "attack_range"));
                CrossBow.range[i] = float.Parse(data.get(i,"attack_range").ToString());
                //Debug.Log("test");
                CrossBow.cost[i] = Convert.ToInt32(data.get(i, "levelup_cost"));
                CrossBow.attack[i] = Convert.ToSingle(data.get(i, "attack"));
                CrossBow.magic_attack[i] = Convert.ToSingle(data.get(i, "magic_attack"));
                CrossBow.crit_dmg[i] = Convert.ToSingle(data.get(i, "crit_dmg"));
                CrossBow.crit_rate[i] = Convert.ToSingle(data.get(i, "crit_rate"));
                CrossBow.rate[i] = Convert.ToSingle(data.get(i, "attack_speed"));
                CrossBow.ID[i] = Convert.ToInt32(data.get(i, "ID"));

                Culverin.range[i] = Convert.ToSingle(data.get(i + 4, "attack_range"));
                Culverin.cost[i] = Convert.ToInt32(data.get(i + 4, "levelup_cost"));
                Culverin.attack[i] = Convert.ToSingle(data.get(i + 4, "attack"));
                Culverin.magic_attack[i] = Convert.ToSingle(data.get(i + 4, "magic_attack"));
                Culverin.crit_dmg[i] = Convert.ToSingle(data.get(i + 4, "crit_dmg"));
                Culverin.crit_rate[i] = Convert.ToSingle(data.get(i + 4, "crit_rate"));
                Culverin.rate[i] = Convert.ToSingle(data.get(i + 4, "attack_speed"));
                Culverin.ID[i] = Convert.ToInt32(data.get(i + 4, "ID"));

                FattyPlasma.range[i] = Convert.ToSingle(data.get(i + 8, "attack_range"));
                Debug.Log("range: " + FattyPlasma.range[i]);
                FattyPlasma.cost[i] = Convert.ToInt32(data.get(i + 8, "levelup_cost"));
                FattyPlasma.attack[i] = Convert.ToSingle(data.get(i + 8, "attack"));
                FattyPlasma.magic_attack[i] = Convert.ToSingle(data.get(i + 8, "magic_attack"));
                FattyPlasma.crit_dmg[i] = Convert.ToSingle(data.get(i + 8, "crit_dmg"));
                FattyPlasma.crit_rate[i] = Convert.ToSingle(data.get(i + 8, "crit_rate"));
                FattyPlasma.rate[i] = Convert.ToSingle(data.get(i + 8, "attack_speed"));
                FattyPlasma.ID[i] = Convert.ToInt32(data.get(i + 8, "ID"));
            }
            catch(System.Exception e)
            {
                Debug.Log("in " + i);
                Debug.LogException(e);
            }
        }

    }

}
