using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "AIEnemyShooterController", menuName = "Controllers/AI/Enemy/Shooter")]
    public class AIEnemyShooterController : AIController
    {
        public string target;
        public bool stay;

        public void Search(AIEnemyShooterBehaviour enemy)
        {
            if (stay)
            {
                return;
            }
            if (enemy.State != CharacterState.Searching)
            {
                enemy.State = CharacterState.Searching;
                enemy.Animator.SetBool("Attack", false);
            }
            GameObject finded = GameObject.FindGameObjectWithTag(target);
            if (finded)
            {
                enemy.NavMeshAgent.SetDestination(finded.transform.position);
            }
            else
            {
                Debug.Log($"[AI] i dont find anything, i dont know what to do.. i gonna cry :(");
            }
        }
        public void Stop(AIEnemyShooterBehaviour enemy)
        {

            if (!enemy.NavMeshAgent.isStopped)
            {
                enemy.NavMeshAgent.isStopped = true;
                enemy.NavMeshAgent.speed = 0;
                Debug.Log($"[AI] i stop move ");
            }
        }
        public void Move(AIEnemyShooterBehaviour enemy)
        {
            if (stay)
            {
                return;
            }
            if (enemy.NavMeshAgent.isStopped)
            {
                enemy.NavMeshAgent.isStopped = false;
                enemy.NavMeshAgent.speed = 3.5f;
            }
        }
        public void Attack(ITakeDamage target, AIEnemyShooterBehaviour enemy)
        {
            if (stay)
            {
                return;
            }
            Debug.Log($"[AI] attacking ");
            if (enemy.State != CharacterState.Attacking)
            {
                enemy.State = CharacterState.Attacking;
                enemy.Animator.SetBool("Attack", true);
            }
            IMakeDamage damage = enemy.Model.GetWeapon(Random.Range(0, enemy.Model.WeaponsCount));
            if (damage != null)
            {
                Debug.Log($"[AI][Controller]Enemy: {enemy.name}");
                if (enemy.Attributes == null)
                {
                    Debug.Log($"[AI][Controller]Enemy Not Have Attributes: {enemy.name}");
                }
                target.Take(damage.Get(enemy.Attributes));
            }
            else
            {
                Debug.Log($"[{enemy.name}] What?!! I dont have any weapon!! :(");
            }
        }

        public void Take(Attributes attributes, AIEnemyShooterBehaviour enemy)
        {
            float damage = attributes.attack;
            damage *= 1.0f / attributes.defense;
            float hitProbability = Random.Range(0, 100) / attributes.dextery;
            if (hitProbability < 30f)
            {
                if (hitProbability < 10)
                {
                    damage *= 2;
                    Debug.Log($"[{enemy.name}] Sweet! is Critical");
                }
                attributes.health -= damage;
                if (attributes.health <= 0)
                {
                    enemy.DiedChannel.TriggerEvent(enemy);
                    enemy.Animator.SetTrigger("Die");
                    Destroy(enemy.gameObject, 5);
                }
            }
            else
            {
                Debug.Log($"[{enemy.name}] What?! I fail!");
            }
        }
    }
}
