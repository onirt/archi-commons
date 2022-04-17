using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void OnMouseDown()
    {
        animator.SetTrigger("Touch");
    }
}
