using System;

[Serializable]
public class UnitDataBackup
{
    public int health;
    public int attackDamage;
    public float moveSpeed;

    public UnitDataBackup(UnitData data)
    {
        health = data.health;
        attackDamage = data.attackDamage;
        moveSpeed = data.moveSpeed;
    }

    public void Restore(UnitData data)
    {
        data.health = health;
        data.attackDamage = attackDamage;
        data.moveSpeed = moveSpeed;
    }
}