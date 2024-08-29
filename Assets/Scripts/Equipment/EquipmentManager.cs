using CSV;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    CSVData data = new CSVData();
    public EquipmentData[] equipments;
    public static Dictionary<int, EquipmentData> equipmentMap;
    // Start is called before the first frame update

    void Awake()
    {
        data.OpenCSV(Application.dataPath + "/Config/ConfigFile_ComponentData.csv");
        int equipNum = data.getRowsNum();
        Debug.Log("equipmentNum:  " + equipNum);
        equipments = new EquipmentData[equipNum];
        equipmentMap = new Dictionary<int, EquipmentData>();
        for (int i = 0; i < equipments.Length; i++)
        {
            equipments[i] = data.DatafromCSV<EquipmentData>(i);
            equipmentMap.Add(Convert.ToInt32(data.get(i, "ID")), equipments[i]);
        }


    }


}
