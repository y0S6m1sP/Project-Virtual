using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    public int money;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else {
            instance = this;
        }

        player = FindObjectOfType<Player>();
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
}
