using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters;
using UnityStandardAssets.Characters.ThirdPerson;

namespace ArChi
{
    public class ThrirdPersonPlayerTouchBehaviour : MonoBehaviour, IDestination
    {
        [Space(20)]
        [SerializeField] private ThirdPersonCharacter thirdPerson;
        [SerializeField] private NavMeshAgent agent;
        void Start()
        {
            agent.updateRotation = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                thirdPerson.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                thirdPerson.Move(Vector3.zero, false, false);
            }
        }
        public void SetDestination(Vector3 destination)
        {
            Debug.Log($"[Game][Touch] destination listened: {destination}");
            agent.SetDestination(destination);
        }
    }
}
