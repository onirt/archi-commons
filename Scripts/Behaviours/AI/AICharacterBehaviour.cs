using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityStandardAssets;
using UnityStandardAssets.Characters.ThirdPerson;

namespace ArChi
{
    [RequireComponent(typeof(NavMeshAgent), typeof(ThirdPersonCharacter))]
    public class AICharacterBehaviour : AIPawnBehaviour, IPoseCharacter
    {
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected ThirdPersonCharacter thirdPersonCharacter;
        [SerializeField] protected Transform headControlReference;
        [SerializeField] protected Transform chestControlReference;
        [SerializeField] protected Transform leftControlReference;
        [SerializeField] protected Transform rightControlReference;
        [SerializeField] protected Rig[] rigs;

        public Transform Head => headControlReference;
        public Transform Chest => chestControlReference;
        public Transform RightHand => rightControlReference;
        public Transform LeftHand => leftControlReference;

        public ThirdPersonCharacter ThirdPersonCharacter { get => thirdPersonCharacter; }
        public NavMeshAgent NavMeshAgent { get => agent; }

        public bool isMoving { get => agent.remainingDistance > agent.stoppingDistance; }

        public override void Init()
        {
            base.Init();
            agent.updateRotation = false;
        }

        public void SetHead(SimpleTransform transform)
        {
            if (!headControlReference) return;
            headControlReference.localPosition = transform.position;
            headControlReference.localRotation = Quaternion.Euler(transform.rotation);

        }

        public void SetChest(SimpleTransform transform)
        {
            if (!chestControlReference) return;
            chestControlReference.localPosition = transform.position;
            chestControlReference.localRotation = Quaternion.Euler(transform.rotation);
        }

        public void SetRightHand(SimpleTransform transform)
        {
            if (!rightControlReference) return;
            rightControlReference.localPosition = transform.position;
            rightControlReference.localRotation = Quaternion.Euler(transform.rotation);
        }

        public void SetLeftHand(SimpleTransform transform)
        {
            if (!leftControlReference) return;
            leftControlReference.localPosition = transform.position;
            leftControlReference.localRotation = Quaternion.Euler(transform.rotation);
        }

        public virtual void SetWeapon(SimpleTransform transform)
        {
            
        }
    }

}
