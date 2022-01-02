using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemy
{
    Normal,

}
[System.Serializable]
public class EnemyConfigRecord
{
    public EEnemy enemy;
    public int health;
    public float detectRange;
    public float attackRange;
    public int damage;
    public float speed;
    public float attackSpeed;
}

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 1)]
public class EnemyConfig : ScriptableObject
{
    public List<EnemyConfigRecord> records;

    public EnemyConfigRecord GetRecordByEnemy(EEnemy enemy)
    {
        return records.Find(s => s.enemy == enemy);
    }
}
