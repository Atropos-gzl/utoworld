using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ThreeSelectOne : MonoBehaviour
{
    public GameObject Invertory;
    
    private Transform[] options;
    
    void Start()
    {
        int i = 0;
        options = new Transform[3];
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name.StartsWith("Option"))
            {
                options[i] = child;
                i++;
            }
            if (i >= 3)
            {
                break;
            }
        }
    }

    public void displayItem(Item []items)
    {
        Start();
        for (int i = 0; i < 3; i++)
        {
            options[i].GetComponent<ItemOption>().item = items[i];
            Sprite sp = GameResourceManager.getSprite(items[i].ID);
            options[i].GetChild(0).GetComponent<Image>().sprite = sp;
            options[i].GetChild(1).GetComponent<Text>().text = items[i].description;
        }
    }

   
}
