using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{ 
    public GameObject[] turretPrafab;
    public GameObject bullet_Prefab;
    public int[] cost = new int[4];
    public float[] attack = new float[4];
    public float[] magic_attack = new float[4];
    public float[] crit_rate = new float[4];
    public float[] crit_dmg = new float[4];
    public float[] range = new float[4];
    public float[] rate = new float[4];
    public int[] ID = new int[4];
    public int level = 0;
    public int speed;
    public TurretType type;

}

public enum TurretType
{
    CrossBow,
    Culverin,
    FattyPlasma
}
