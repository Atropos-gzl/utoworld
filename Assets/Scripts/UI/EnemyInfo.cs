using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    public Text Defence;
    public Text MagicDefence;
    public Text Speed;
    public Text Attack;
    public Image Enemy;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "Monster")
            {
                Enemy = child.GetComponent<Image>();
            }
            if (child.name == "Attack")
            {
                Attack = child.GetComponent<Text>();
            }
            if (child.name == "Speed")
            {
                Speed = child.GetComponent<Text>();
            }
            if (child.name == "Defence")
            {
                Defence = child.GetComponent<Text>();
            }
            if (child.name == "MagicDefence")
            {
                MagicDefence = child.GetComponent<Text>();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            Attack.text = enemy.GetComponent<Enemy>().attack.ToString();
            Speed.text = enemy.GetComponent<Enemy>().movespeed.ToString();
            Defence.text = enemy.GetComponent<Enemy>().defence.ToString();
            MagicDefence.text = enemy.GetComponent<Enemy>().magicdefence.ToString();
        }
    }
}
