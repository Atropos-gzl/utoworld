using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CSV;

public class MapManager : Singleton<MapManager>
{
    private Dictionary<int, List<EquipmentData>> _chestEquipmentMap;
    public Dictionary<int, List<EquipmentData>> chestEquipmentMap
    {
        get { 
            if (_chestEquipmentMap == null)
            {
                if (EquipmentManager.equipmentMap == null)
                    return null;
                _chestEquipmentMap = new Dictionary<int, List<EquipmentData>>();
                foreach(var equ in EquipmentManager.equipmentMap.Values)
                {
                    if (!_chestEquipmentMap.ContainsKey(equ.component_level))
                    {
                        _chestEquipmentMap[equ.component_level] = new List<EquipmentData>();
                    }
                    _chestEquipmentMap[equ.component_level].Add(equ);
                }
            }
            return _chestEquipmentMap; 
        }
    }

    private Dictionary<int, List<ChestData>> _chestDataMap;
    public Dictionary<int, List<ChestData>> chestDataMap
    {
        get
        {
            if (_chestDataMap == null)
            {
                CSVData data = new CSVData();
                data.OpenCSV(Application.dataPath + "/Config/ConfigFile_ChestData.csv");
                _chestDataMap = new Dictionary<int, List<ChestData>>();
                int chestNum = data.getRowsNum();
                for (int i = 0; i < chestNum; i++)
                {
                    ChestData tmpData = data.DatafromCSV<ChestData>(i);
                    if (!_chestDataMap.ContainsKey(tmpData.ID))
                    {
                        _chestDataMap[tmpData.ID] = new List<ChestData>();
                    }
                    _chestDataMap[tmpData.ID].Add(tmpData);
                }
                
            }
            return _chestDataMap;
        }
    }

    public int RandomEquipFromLevel(int level)
    {
        var list = chestEquipmentMap[level];
        int random_res = Random.Range(0, list.Count);
        return list[random_res].ID;
    }
    public int[] RandomEquipFromChest(int item_id)
    {
        var data = chestDataMap[item_id];
        if (data == null)
        {
            return null;
        }
        float random_res = Random.value;
        foreach(var item in data)
        {
            if (item.rate >= random_res)
            {
                int[] ret = { 0, 0, 0 };
                ret[0] = RandomEquipFromLevel(item.component_level1);
                ret[1] = RandomEquipFromLevel(item.component_level2);
                ret[2] = RandomEquipFromLevel(item.component_level3);
                return ret;
            }
            random_res -= item.rate;
        }
        return null;
    }
}


public class ChestData
{
    public int ID { set; get; }
    public int component_level1 { set; get; }
    public int component_level2 { set; get; }
    public int component_level3 { set; get; }
    public float rate { set; get; }

    public ChestData(int _ID, int _component_level1, int _component_level2, int _component_level3, float _rate)
    {
        this.ID = _ID;
        this.component_level1 = _component_level1;
        this.component_level2 = _component_level2;
        this.component_level3 = _component_level3;
        this.rate = _rate;
    }

    public ChestData() { }
}
