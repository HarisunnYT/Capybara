using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakeTween
{
    public float Duration = 1;
    public float Strength = 90;
    public int Vibrato = 10;
    public float Randomness = 90;
}

public abstract class DamagableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float startingHealth;

    [Space()]
    public float ReadOnlyHealth;

    public float CurrentHealth { get; set; }

    protected virtual void Awake()
    {
        CurrentHealth = startingHealth;
        ReadOnlyHealth = CurrentHealth;
    }

    public void Damaged(float amount)
    {
        CurrentHealth -= amount;
        ReadOnlyHealth = CurrentHealth;

        OnDamaged();

        if (CurrentHealth <= 0)
        {
            OnDestroyed();
        }
    }

    protected virtual void OnDestroyed() { }
    protected virtual void OnDamaged() { }
}
