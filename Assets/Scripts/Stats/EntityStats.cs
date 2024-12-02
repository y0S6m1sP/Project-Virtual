using System.Collections;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private EntityFX Fx => GetComponent<EntityFX>();

    public string entityName;

    [Space]
    public Stat health;
    public Stat mana;
    public Stat damage;
    public Stat armor;

    [Space]
    public int currentHealth;
    public int currentArmor;
    public float currentMana;

    [Space]
    public Vector2 knockbackPower = new(7, 0);

    public System.Action onHealthChanged;
    public System.Action onManaChanged;
    public System.Action onArmorChanged;
    public bool IsDead { get; private set; }

    protected virtual void Start()
    {
        currentHealth = health.GetValue();
        currentArmor = armor.GetValue();
        currentMana = mana.GetValue();

        // StartCoroutine(DecreaseManaOverTime());
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

    public virtual void IncreaseHealthBy(int _amount)
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

    public virtual void IncreaseManaBy(float _amount)
    {
        currentMana += _amount;
        onManaChanged?.Invoke();
    }

    public virtual void DecreaseManaBy(float _amount)
    {
        currentMana -= _amount;
        if(currentMana < 0) currentMana = 0;
        onManaChanged?.Invoke();
    }

    private IEnumerator DecreaseManaOverTime()
    {
        while (!IsDead)
        {
            DecreaseManaBy(Time.deltaTime);
            yield return null; // Wait for the next frame
        }
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