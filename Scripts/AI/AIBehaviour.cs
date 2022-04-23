using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour, IAIBehaviour
{
    [SerializeField] private Animator animator;

    public void Init()
    {
        animator.SetBool("Started", true);
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("Touch");
    }
}
