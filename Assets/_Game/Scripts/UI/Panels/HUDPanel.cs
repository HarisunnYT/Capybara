using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDPanel : Panel
{
    [SerializeField]
    private GameObject crosshair;

    [SerializeField]
    private TMP_Text currencyText;

    private void Start()
    {
        CurrencyManager.Instance.OnCurrencyModified += UpdateCurrencyText;
        currencyText.text = CurrencyManager.Instance.Currency.ToString();
    }

    private void UpdateCurrencyText(int oldAmount, int newAmount)
    {
        Util.PunchIntigerCounter(currencyText, 0.2f, oldAmount, newAmount);
    }

    public void ShowCrosshair(bool show)
    {
        crosshair.SetActive(show);
    }
}
