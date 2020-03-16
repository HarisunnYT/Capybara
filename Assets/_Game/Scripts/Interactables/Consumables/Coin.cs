using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int amount = 1;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CurrencyManager.Instance.AddCurrency(amount);
            gameObject.SetActive(false);
        }
    }
}
