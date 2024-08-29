using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOption : MonoBehaviour
{
    public GameObject invertoryManager;
   

    public Item item;

    public void addItem()
    {
        
        Invertory invertory = invertoryManager.GetComponent<Invertory>();
        invertory.addItem(Convert.ToInt32(item.ID));
        Debug.Log("sahgdu");
        Debug.Log(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
