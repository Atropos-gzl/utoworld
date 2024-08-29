using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Prop
{
    public GameObject treasureTipPrefab;
    public GameObject equipmentSelectPrefab;
    public GameObject treasureTip;
    public GameObject equipmentSelect;

    // Start is called before the first frame update

    private void Awake()
    {
        treasureTip = Instantiate(treasureTipPrefab);
        equipmentSelect = Instantiate(equipmentSelectPrefab);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
