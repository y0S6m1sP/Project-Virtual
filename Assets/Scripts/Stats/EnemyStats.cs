using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyStats : EntityStats
{
    private Enemy enemy;

    [Header("Item Drop")]
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private int moneyAmount;

    override protected void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        AudioManager.instance.PlaySFX(Random.Range(3, 5));
    }

    protected override void Die()
    {
        base.Die();

        for (int i = 0; i < moneyAmount / 10; i++)
        {
            GameObject money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
            money.GetComponent<MoneyController>().Setup(moneyAmount / 10);
        }

        enemy.Die();
    }
}