using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi.Controllers
{
    [CreateAssetMenu(fileName = "AIEnemyShooterController", menuName = "Controllers/AI/Enemy/Shooter")]
    public class AIEnemyShooterController : AIShooterController
    {
        [Header("Channels")]
        [SerializeField] private Vecto3DelegateChannel patrolPointsChannel;
        public void Search(AIEnemyShooterBehaviour enemy)
        {
            if (enemy.haveAim)
            {
                Vector3 otherPosition = enemy.TargetAim.target.position;
                if (Vector3.Distance(otherPosition, enemy.transform.position) > enemy.GetRange())
                {
                    enemy.NavMeshAgent.SetDestination(new Vector3(otherPosition.x, 0, otherPosition.z));
                    enemy.state = CharacterState.Seeking;
                    Move(enemy);
                }
                else
                {
                    enemy.state = CharacterState.Attacking;
                    Stop(enemy);
                }
            }
            else
            {
                enemy.state = CharacterState.Idle;
                Stop(enemy);
            }
        }
        public void Think(AIEnemyShooterBehaviour enemy)
        {
            if (enemy.exhaustionColdown > 0 && enemy.exhaustionColdown < enemy._Model.Stamina)
            {
                enemy.exhaustionColdown -= Time.deltaTime;
                if (enemy.exhaustionColdown < 0)
                {
                    enemy.exhaustionColdown = 0;
                    enemy.PerceptionRadius = enemy.GetRange();
                }
            }
            if (Random.Range(0, 3) == 0)
            {
                enemy.state = CharacterState.Patrol;
            }
            else
            {
                enemy.state = CharacterState.Idle;
            }
        }
        public void Seek(AIEnemyShooterBehaviour enemy)
        {
            if (enemy.haveAim)
            {
                float distance = Vector3.Distance(enemy.transform.position, enemy.TargetAim.target.position);
                if (distance > enemy.GetRange())
                {
                    enemy.NavMeshAgent.SetDestination(enemy.TargetAim.target.position);
                    Move(enemy);
                }
                else
                {
                    enemy.state = CharacterState.Attacking;
                }
            }
            else
            {
                if (enemy.exhaustionColdown < enemy._Model.Stamina)
                {
                    enemy.PerceptionRadius = GetPerception(enemy);
                    enemy.exhaustionColdown += Time.deltaTime;
                }
                else
                {
                    enemy.PerceptionRadius = enemy.GetRange();
                    enemy.state = CharacterState.Idle;
                    Stop(enemy);
                }
            }
        }
        public void Patrol(AIEnemyShooterBehaviour enemy)
        {
            if (!enemy.isMoving)
            {
                enemy.NavMeshAgent.SetDestination(patrolPointsChannel.Get());
                enemy.state = CharacterState.Idle;
            }
            Move(enemy);
        }
        private float GetPerception(AIEnemyShooterBehaviour enemy)
        {
            return enemy.GetRange() + enemy.GetModel().Attributes.dextery * 0.5f;
        }
    }
}
