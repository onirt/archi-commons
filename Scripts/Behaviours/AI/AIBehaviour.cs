using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIBehaviour : MonoBehaviour, IAIBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] private AnimationClip[] clips;

        public CharacterState state;

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
            if (animator)
            {
                animator.SetBool("Started", true);
            }
            state = CharacterState.Idle;
        }
    }
    public enum CharacterState
    {
        Idle,
        Attacking,
        Searching,
        Seeking,
        Patrol,
        Defending
    }

    [Serializable]
    public struct SimpleVector
    {
        public float x;
        public float y;
        public float z;
    }
    [Serializable]
    public struct SimpleTransform
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    [Serializable]
    public class Pose
    {
        public PoseReference reference;
        public SimpleTransform transform;
    }
    public enum PoseReference
    {
        Head,
        Chest,
        Lefthand,
        RightHand,
        Weapon
    }
}