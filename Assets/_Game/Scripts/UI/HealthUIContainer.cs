using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIContainer : MonoBehaviour
{
    [SerializeField]
    private Image[] hearts;

    private void Start()
    {
        GameManager.Instance.CapyController.HealthController.OnHealthChanged += UpdateHearts;
    }

    private void UpdateHearts(float health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartNumber = i + 1;
            if (heartNumber > health && health > heartNumber - 1)
            {
                hearts[i].fillAmount = (health % 1);
            }
            if (health > heartNumber)
            {
                hearts[i].fillAmount = 1;
            }

            float temp = heartNumber - health;
            if (temp > 1)
            {
                hearts[i].fillAmount = 0;
            }

            if (heartNumber == health)
            {
                hearts[i].fillAmount = 1;
            }

            if (health % 1 == 0)
            {
                if (heartNumber > health)
                {
                    hearts[i].fillAmount = 0;
                }
            }
        }
    }
}
