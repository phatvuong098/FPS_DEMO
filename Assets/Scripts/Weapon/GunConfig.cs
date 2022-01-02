using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunConfigRecord
{
    public EWeapon eWeapon;
    public int clipSize;
    public int total;
    public float rof;
    public int damage;
    public float acuracy;
}

[CreateAssetMenu(fileName = "GunConfig", menuName = "ScriptableObjects/GunConfig", order = 1)]
public class GunConfig : ScriptableObject
{
    public List<GunConfigRecord> records;

    public GunConfigRecord GetConfigByEWeapon(EWeapon eWeapon)
    {
        return records.Find(s => s.eWeapon == eWeapon);
    }
}