using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi.Controllers
{
    public class AIShooterController : AICharacterController
    {
        public void Stop(AIShooterBehaviour shooter)
        {
            shooter.NavMeshAgent.SetDestination(shooter.transform.position);
            shooter.ThirdPersonCharacter.Move(Vector3.zero, false, false);
        }
        public void Take(Attributes attributes, AIShooterBehaviour shooter)
        {
            float damage = attributes.attack;
            damage *= 1.0f / attributes.defense;
            float hitProbability = UnityEngine.Random.Range(0, 100) / attributes.dextery;
            if (hitProbability < 30f)
            {
                if (hitProbability < 10)
                {
                    damage *= 2;
                    Debug.Log($"[{shooter.name}] Sweet! is Critical");
                }
                attributes.health -= damage;
                if (attributes.health <= 0)
                {
                    shooter.DiedChannel.TriggerEvent(shooter);
                    shooter.Animator.SetTrigger("Die");
                    Destroy(shooter.gameObject, 5);
                }
            }
            else
            {
                Debug.Log($"[{shooter.name}] What?! I fail!");
            }
        }

        public void Attack(ITakeDamage target, AIShooterBehaviour shooter)
        {
            if (shooter.coldown > shooter.GetModel().Attributes.cooldown)
            {
                shooter.coldown = 0;
                shooter.transform.LookAt(shooter.TargetAim.target, Vector3.up);
                Debug.Log($"[AI] attacking ");
                
                SetPose(1, shooter);
                
                IMakeDamage damage = shooter._Model.GetWeaponDamage(UnityEngine.Random.Range(0, shooter._Model.WeaponsCount));
                if (damage != null)
                {
                    if (shooter.Attributes == null)
                    {
                        Debug.Log($"[AI][Controller]Enemy Not Have Attributes: {shooter.name}");
                    }
                    target.Take(damage.Get(shooter.Attributes));
                }
                else
                {
                    Debug.Log($"[{shooter.name}] What?!! I dont have any weapon!! :(");
                }
            }
            else
            {
                shooter.coldown += Time.deltaTime;
            }
            Stop(shooter);
        }
    }
}
