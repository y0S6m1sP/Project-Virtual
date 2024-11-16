using System.Collections;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private EntityFX Fx => GetComponent<EntityFX>();

    public Stat maxHealth;
    public Stat damage;
    public Stat armor;
    public int currentHealth;
    public int currentArmor;

    public System.Action onHealthChanged;
    public System.Action onArmorChanged;
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
            DecreaseArmorBy(_damage);
            return;
        }

        DecreaseHealthBy(_damage);

        if (currentHealth <= 0 && !IsDead) Die();
    }

    protected virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;
        onHealthChanged?.Invoke();
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;
        Fx.CreatePopupText(_damage.ToString());
        onHealthChanged?.Invoke();
    }

    protected virtual void IncreaseArmorBy(int _amount)
    {
        currentArmor += _amount;
        onArmorChanged?.Invoke();
    }

    protected virtual void DecreaseArmorBy(int _damage)
    {
        currentArmor -= _damage;
        onArmorChanged?.Invoke();
    }


    protected virtual void Die()
    {
        IsDead = true;
    }

}