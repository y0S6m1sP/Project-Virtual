using System.Collections;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat damage;
    public Stat armor;
    public int currentHealth;
    public int currentArmor;
    public bool IsDead { get; private set; }

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
        currentArmor = armor.GetValue();
    }

    public virtual void DoDamage(EntityStats _entityStats)
    {
        _entityStats.TakeDamage(damage.GetValue());
    }

    public virtual void TakeDamage(int _damage)
    {
        if (currentArmor > 0)
        {
            currentArmor--;
            return;
        }
        
        currentHealth -= _damage;

        if (currentHealth <= 0 && !IsDead) Die();
    }

    protected virtual void Die()
    {
        IsDead = true;
    }

}