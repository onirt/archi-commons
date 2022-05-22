using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi.Controllers
{
    public class AICharacterController : AIController<AICharacterBehaviour>
    {
        [SerializeField] protected CharacterRigPose[] characterPose;

        public string target;
        public bool stay;

        public override void Move(AICharacterBehaviour character)
        {
            if (character.isMoving)
            {
                character.ThirdPersonCharacter.Move(character.NavMeshAgent.desiredVelocity, false, false);
            }
            else
            {
                character.ThirdPersonCharacter.Move(Vector3.zero, false, false);
            }
        }
        public void SetPose(int index, AIShooterBehaviour chacrter)
        {
            if (index < characterPose.Length)
            {
                characterPose[index].Set(chacrter);
            }
            else
            {
                Debug.LogError("[AI][Character][Controller] index pose is out of range!!");
            }
        }
    }
}
