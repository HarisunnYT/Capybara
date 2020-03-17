using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDispenser : MonoBehaviour
{
    [SerializeField]
    private int minCoins, maxCoins;

    [Space()]
    [SerializeField]
    private float force;

    [SerializeField]
    private GameObject coinPrefab;

    public void DispenseCoins()
    {
        int randomAmount = Random.Range(minCoins, maxCoins);

        DispenseCoins(randomAmount);
    }

    public void DispenseCoins(int exactAmount)
    {
        for (int i = 0; i < exactAmount; i++)
        {
            GameObject coin = ObjectPooler.GetPooledObject(coinPrefab);
            Rigidbody rigidbody = coin.GetComponent<Rigidbody>();

            coin.transform.position = transform.position;

            Vector3 r = (Random.insideUnitSphere * 360).normalized;

            rigidbody.AddForce(r * force, ForceMode.Impulse);
        }
    }
}
