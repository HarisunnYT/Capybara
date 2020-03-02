using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IDamageable
{
    int CurrentHealth { get; set; }

    void OnDamaged(int amount);
}

public class HealthController : Controller, IDamageable
{
    [SerializeField]
    private int maxHealth = 10;

    public int CurrentHealth { get; set; }

    protected override void Awake()
    {
        base.Awake();

        CurrentHealth = maxHealth;
    }

    public void OnDamaged(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0 && MovementController.CurrentMovementState != MovementState.Dead)
        {
            CharacterController.Die();
        }
    }
}