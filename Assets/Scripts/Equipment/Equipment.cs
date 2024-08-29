using System;
using System.Collections;
using System.Collections.Generic;
using CSV;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    

    // Update is called once per frame

}

[System.Serializable]
public class EquipmentData
{
    //public float ID;
    //public float attack;
    //public float magic_attack;
    //public float attack_speed;
    //public float attack_range;
    //public float crit_rate;
    //public float crit_dmg;
    //public float install_cost;
    //public float component_level;
    public int ID { set; get; }
    public float attack { set; get; }
    public float magic_attack { set; get; }
    public float attack_speed { set; get; }
    public float attack_range { set; get; }
    public float crit_rate { set; get; }
    public float crit_dmg { set; get; }
    public int install_cost { set; get; }
    public int component_level { set; get; }
    public string func { set; get; }
    public string description { set; get; }


    public EquipmentData(int id, float attack, float magic_attack, float attack_speed, float attack_range, float crit_rate, float crit_dmg, int install_cost, int component_level, string func, string description)
    {
        this.ID = id;
        this.attack = attack;
        this.magic_attack = magic_attack;
        this.attack_speed = attack_speed;
        this.attack_range = attack_range;
        this.crit_rate = crit_rate;
        this.crit_dmg = crit_dmg;
        this.install_cost = install_cost;
        this.component_level = component_level;
        this.func = func;
        this.description = description;
    }

    public EquipmentData() { }



}
