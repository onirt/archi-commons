using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour, IAIBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.SetBool("Started", true);
    }
    public void Init()
    {
        Debug.Log("[AIBehaviour] Init");
        animator.SetBool("Started", true);
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("Touch");
    }
}
