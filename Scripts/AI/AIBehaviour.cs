using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour, IAIBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip[] clips;

    public Attributes attributes;
    public void Start()
    {
        Init();
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("Touch");
    }
    public virtual void Move(Vector3 direction)
    {

    }

    public void Init()
    {
        Debug.Log("[AIBehaviour] Init");
        animator.SetBool("Started", true);
    }
}
