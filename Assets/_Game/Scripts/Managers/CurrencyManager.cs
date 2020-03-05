using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public int Currency { get; private set; } = 0;

    public delegate void CurrencyEvent(int previousAmount, int newAmount);
    public event CurrencyEvent OnCurrencyModified;

    public void AddCurrency(int amount)
    {
        ModifyCurrency(amount);
    }

    public void RemoveCurrency(int amount)
    {
        ModifyCurrency(-amount);
    }

    public void ClearCurrency()
    {
        RemoveCurrency(Currency);
    }

    private void ModifyCurrency(int amount)
    {
        Currency = Mathf.Clamp(Currency + amount, 0, int.MaxValue);
        OnCurrencyModified?.Invoke(Currency - amount, Currency);
    }
}
