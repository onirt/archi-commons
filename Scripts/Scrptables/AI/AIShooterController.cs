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
                    Debug.Log($"[Shoot][{shooter.name}] Sweet! is Critical");
                }
                Attributes shooterAttributes = shooter.Attributes;
                Debug.Log($"1.[Shoot][Damage][{shooter.name}] health: {shooterAttributes.health}, damage: {damage}");
                shooterAttributes.health -= damage;
                shooter.Attributes = shooterAttributes;
                shooter.ui.UpdatedHealth(shooterAttributes.health);
                Debug.Log($"2.[Shoot][Damage][{shooter.name}] health: {shooterAttributes.health}, damage: {damage}");
                if (shooterAttributes.health <= 0)
                {
                    Debug.Log($"[{shooter.name}] Ohhh i Died ughhh!");
                    shooter.DiedChannel.TriggerEvent(shooter);
                    shooter.Animator.SetTrigger("Die");
                    Destroy(shooter.gameObject, 5);
                }
            }
            else
            {
                Debug.Log($"[Shoot][Damage][{shooter.name}] What?! I fail! [{hitProbability}]");
            }
        }

        public void Attack(ITakeDamage target, AIShooterBehaviour shooter)
        {
            if (shooter.coldown > shooter.GetModel().Attributes.cooldown)
            {
                shooter.coldown = 0;
                Vector3 lookAt = shooter.TargetAim.target.position;
                shooter.transform.LookAt(lookAt, Vector3.up);
                lookAt = new Vector3(lookAt.x, shooter.Weapon.position.y, lookAt.z);
                shooter.Weapon.LookAt(lookAt);
                Debug.Log($"[AI] attacking ");
                
                SetPose(1, shooter);

                shooter.Weapon.LookAt(lookAt, Vector3.up);

                IMakeDamage damage = shooter.GetWeaponDamage();
                if (damage != null)
                {
                    shooter.Shoot(damage);
                }
                else
                {
                    target.Take(shooter._Model.Attributes);
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
