using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IDamageable
{
    float CurrentHealth { get; set; }

    void Damaged(float amount);
}

public class HealthController : Controller, IDamageable
{
    [SerializeField]
    private int maxHealth = 10;
    public int MaxHealth { get { return maxHealth; } }

    public float CurrentHealth { get; set; }
    public float HealthReadOnly;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Color originalMeshColor;
    private Tween flashTween;

    public delegate void HealthChangeEvent(float health);
    public event HealthChangeEvent OnHealthChanged;

    protected override void Awake()
    {
        base.Awake();

        CurrentHealth = maxHealth;
        HealthReadOnly = CurrentHealth;

        OnHealthChanged?.Invoke(CurrentHealth);

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalMeshColor = skinnedMeshRenderer.material.GetColor("_BaseColor");
    }

    public void Healed(float amount)
    {
        CurrentHealth += amount;

        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void Damaged(float amount)
    {
        CurrentHealth -= amount;
        HealthReadOnly = CurrentHealth;

        if (amount > 0)
        {
            FlashRed();
        }

        if (CurrentHealth <= 0 && MovementController.CurrentMovementState != MovementState.Dead)
        {
            CharacterController.Die();
        }

        OnHealthChanged?.Invoke(CurrentHealth);
    }

    private void FlashRed()
    {
        if (flashTween != null)
        {
            flashTween.Kill();
        }

        Color color = Color.red;
        skinnedMeshRenderer.material.SetColor("_BaseColor", color);

        flashTween = DOTween.To(() => color, x => color = x, originalMeshColor, 1).OnUpdate(() =>
        {
            skinnedMeshRenderer.material.SetColor("_BaseColor", color);
        }).OnComplete(() =>
        {
            flashTween = null;
        });
    }
}