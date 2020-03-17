using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : Consumable
{
    [Space()]
    [SerializeField]
    private int amount;

    protected override void OnPickedUp()
    {
        CurrencyManager.Instance.AddCurrency(amount);
        transform.gameObject.SetActive(false);
    }
}
