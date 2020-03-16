using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CashMachine : DamagableObject
{
    [SerializeField]
    private ShakeTween shakeTweenVariables;

    private CoinDispenser coinDispenser;

    private Tween shakeTween;

    protected override void Awake()
    {
        base.Awake();

        coinDispenser = GetComponentInChildren<CoinDispenser>();
    }

    protected override void OnDamaged()
    {
        coinDispenser.DispenseCoins(transform.forward, 10);

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
}
