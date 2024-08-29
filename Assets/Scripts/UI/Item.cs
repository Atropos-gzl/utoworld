using UnityEngine;
using CSV;

public class Item 
{
    public string ID{ get; set; }
    public float attack{ get; set; }
    public float magic_attack{ get; set; }

    public float attack_speed{ get; set; }
    public float attack_range{ get; set; }
    public float crit_rate{ get; set; }
    public float crit_dmg{ get; set; }
    public int install_cost{ get; set; }
    public float component_level{ get; set; }
    public string func{ get; set; }
    public string description{ get; set; } 

    public Item(){}

    public Item(string id, float attack, float magicAttack, float attackSpeed, float attackRange, float critRate, float critDmg, int installCost, float componentLevel, 
        string func, string description)
    {
        ID = id;
        this.attack = attack;
        magic_attack = magicAttack;
        attack_speed = attackSpeed;
        attack_range = attackRange;
        crit_rate = critRate;
        crit_dmg = critDmg;
        install_cost = installCost;
        component_level = componentLevel;
        this.func = func;
        this.description = description;
    }
}