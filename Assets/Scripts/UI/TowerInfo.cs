using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    public GameObject turret;
    public Text attack;
    public Text magic_attack;
    public Text attack_speed;
    public Text attack_range;
    public Text crit_rate;
    public Text crit_dmg;
    public Image Tower;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if(child.name == "Tower")
            {
                Tower = child.GetComponent<Image>();
            }
            if (child.name == "Attack") {
                attack = child.GetComponent<Text>();
            }
            if (child.name == "MagicAttack")
            {
                magic_attack = child.GetComponent<Text>();
            }
            if (child.name == "AttackSpeed")
            {
                attack_speed = child.GetComponent<Text>();
            }
            if (child.name == "AttackRange")
            {
                attack_range = child.GetComponent<Text>();
            }
            if (child.name == "CritRate")
            {
                crit_rate = child.GetComponent<Text>();
            }
            if (child.name == "CritDamage")
            {
                crit_dmg = child.GetComponent<Text>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (turret != null)
        {
            
            attack.text = turret.GetComponent<Turret>().attack.ToString();
            magic_attack.text = turret.GetComponent<Turret>().magic_attack.ToString();
            attack_speed.text = turret.GetComponent<Turret>().rate.ToString();
            attack_range.text = turret.GetComponent<Turret>().attack_range.ToString();
            crit_rate.text = turret.GetComponent<Turret>().crit_rate.ToString();
            crit_dmg.text = turret.GetComponent<Turret>().crit_dmg.ToString();

        }
    }
}
