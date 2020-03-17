using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CashMachine : DamagableObject
{
    [Space()]
    [SerializeField]
    private float forceToDamage = 20;

    [SerializeField]
    private ShakeTween shakeTweenVariables;

    private CoinDispenser coinDispenser;

    private Tween shakeTween;

    private float forceHitDelayTimer;

    private const float forceHitDelay = 0.2f;

    protected override void Awake()
    {
        base.Awake();

        coinDispenser = GetComponentInChildren<CoinDispenser>();
    }

    protected override void OnDamaged()
    {
        coinDispenser.DispenseCoins();

        if (shakeTween != null && !shakeTween.IsComplete())
        {
            shakeTween.Kill();
        }

        shakeTween = transform.DOShakeRotation(shakeTweenVariables.Duration, shakeTweenVariables.Strength, shakeTweenVariables.Vibrato, shakeTweenVariables.Randomness);
    }

    protected override void OnDestroyed()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > forceHitDelayTimer && collision.relativeVelocity.magnitude > forceToDamage)
        {
            forceHitDelayTimer = Time.time + forceHitDelay;
            Damaged(1);
        }
    }
}
