using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent OnPressed;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator)
        {
            animator.SetBool("Hovering", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator)
        {
            animator.SetBool("Hovering", false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPressed?.Invoke();
    }
}
