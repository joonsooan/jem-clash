using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData")]
public class UnitData : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float moveSpeed;
}