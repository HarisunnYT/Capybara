using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDispenser : MonoBehaviour
{
    [SerializeField]
    private int minCoins, maxCoins;

    [SerializeField]
    private GameObject coinPrefab;

    public void DispenseCoins(Vector3 direction, float force)
    {
        int randomAmount = Random.Range(minCoins, maxCoins);

        DispenseCoins(direction, force, randomAmount);
    }

    public void DispenseCoins(Vector3 direction, float force, int exactAmount)
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
