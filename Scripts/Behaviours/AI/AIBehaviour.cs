using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIBehaviour : MonoBehaviour, IAIBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] private AnimationClip[] clips;

        protected CharacterState state;

        public Animator Animator { get => animator; }
        public CharacterState State { get => state; set => state = value; }

        public void Start()
        {
            Init();
        }

        public void OnMouseDown()
        {
            animator.SetTrigger("Touch");
        }

        public virtual void Init()
        {
            Debug.Log("[AIBehaviour] Init");
            animator.SetBool("Started", true);
        }
    }
    public enum CharacterState
    {
        Idle,
        Attacking,
        Searching,
        Defending
    }

}