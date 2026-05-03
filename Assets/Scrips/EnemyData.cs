using UnityEngine;



[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public float patrolRange;
    public string enemyName;
    public float maxHp;
    public float moveSpeed;
    public float damage;
    public float chaseRange;
    public float stopDistanceToPlayer;
}

