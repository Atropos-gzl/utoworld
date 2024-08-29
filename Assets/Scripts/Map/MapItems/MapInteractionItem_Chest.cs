using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractionItem_Chest : MapInteractionItem
{
    public int item_id = 40101;


    public override void InteractionAction()
    {
        int[] items = MapManager.Instance.RandomEquipFromChest(item_id);

        var manager = GameObject.Find("InvertoryManager");
        var invertory = manager.GetComponent<Invertory>();
        invertory?.GetItem(items);
    }


}
