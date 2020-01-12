using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hovering", false);
    }

    public void ButtonEvent_Play()
    {
        SceneManager.LoadScene(1);
    }
}
