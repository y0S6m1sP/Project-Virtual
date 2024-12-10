using System.Collections;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private EntityFX Fx => GetComponent<EntityFX>();

    public string entityName;

    [Space]
    public Stat health;
    public Stat mana;
    public Stat physicDamage;
    public Stat magicDamage;
    public Stat armor;
    public Stat focus;

    [Space]
    public Stat brutal;
    public Stat survive;
    public Stat mystic;

    [Space]
    public int currentHealth;
    public int currentArmor;
    public float currentMana;
    public int currentFocus;

    [Space]
    public Vector2 knockbackPower = new(7, 0);

    public System.Action onHealthChanged;
    public System.Action onManaChanged;
    public System.Action onArmorChanged;
    public System.Action onFocusChanged;
    public bool IsDead { get; private set; }

    protected virtual void Start()
    {
        currentHealth = health.GetValue();
        currentArmor = armor.GetValue();
        currentMana = 0;

        StartCoroutine(DecreaseManaOverTime());
    }

    public virtual void DoDamage(EntityStats _entityStats)
    {
        _entityStats.TakeDamage(physicDamage.GetValue());
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
        onHealthChanged?.Invoke();
    }

    public virtual void IncreaseMana()
    {
        currentMana += mana.GetValue() / 2;
        onManaChanged?.Invoke();
    }

    public virtual void IncreaseManaBy(float _amount)
    {
        currentMana += _amount;
        onManaChanged?.Invoke();
    }

    public virtual void DecreaseManaBy(float _amount)
    {
        currentMana -= _amount;
        if (currentMana < 0) currentMana = 0;
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

    public virtual int IncreaseFocusBy(int _amount)
    {
        var maxFocus = focus.GetValue();
        var newFocus = currentFocus + _amount;

        if (newFocus > maxFocus)
        {
            currentFocus = (newFocus % maxFocus) - 1;
            Debug.Log("Focus is full" + currentFocus);
            onFocusChanged?.Invoke();
            return newFocus / maxFocus;
        }

        currentFocus += _amount;
        Debug.Log("Focus is " + currentFocus);
        onFocusChanged?.Invoke();

        return 0;
    }

    public virtual void DecreaseFocusBy(int _amount)
    {
        currentFocus -= _amount;
        onFocusChanged?.Invoke();
    }

    protected virtual void Die()
    {
        IsDead = true;
    }

    public void TakePhysicalDamage(EntityStats stats)
    {

        int damage = stats.physicDamage.GetValue();
        int critChance = stats.brutal.GetValue() * 5;
        int critDamage = stats.brutal.GetValue() * 10;

        if (Random.Range(0, 100) < critChance)
        {
            damage += damage * critDamage / 100;
            Fx.CreatePopupText(damage.ToString());
        }

        Fx.CreatePopupText(damage.ToString());
        TakeDamage(damage);
    }

    public string GetHealthText()
    {
        return currentHealth + "/" + health.GetValue();
    }

}