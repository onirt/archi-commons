using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace ArChi
{
    public class AIEnemyShooterBehaviour : AIPawnBehaviour, IScore, ITakeDamage
    {
        [Header("Channels")]
        [SerializeField] private AIBehaviourEventChannel iDiedChannel;

        [Header("Controllers")]
        public AIEnemyShooterController controller;

        [Header("Data")]
        public EnemyModel model;
        [Space(20)]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] Transform weaponSpot;

        public EnemyModel Model { get => model; }
        public NavMeshAgent NavMeshAgent { get => agent; }
        public AIBehaviourEventChannel DiedChannel { get => iDiedChannel; }

        private float coldown;

        private struct Aim
        {
            public Transform target;
            public ITakeDamage damage;
        }
        private Aim onAim;
        private bool haveAim;

        public override void Init()
        {
            base.Init();
            attributes = new Attributes(model.Attributes);
            int selected = Random.Range(0, model.WeaponsCount);
            model.InstantiateWeapon(selected, weaponSpot);
        }

        private void Update()
        {
            if (controller.stay)
            {
                return;
            }
            if (haveAim)
            {
                if (coldown > model.Attributes.cooldown)
                {
                    coldown = 0;
                    transform.LookAt(onAim.target, Vector3.up);
                    controller.Attack(onAim.damage, this);
                }
                else
                {
                    coldown += Time.deltaTime;
                }
            }
            else
            {
                controller.Search(this);
                animator.SetFloat("Speed", agent.speed);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[AI] detected in range:[{other.name}]");
            if (other.CompareTag(controller.target))
            {
                Debug.Log($"[AI] Is the Player i gonna kill him!!!!");
                if (other.TryGetComponent(out ITakeDamage take))
                {
                    if (!haveAim)
                    {
                        controller.Stop(this);
                        haveAim = true;
                        Debug.Log($"[AI] I got you in my aim buahahha");
                        onAim = new Aim { target = other.transform, damage = take };
                    }
                }
                else
                {
                    Debug.Log($"[AI] Mmmm there is no way to kill the Player... he is inmortal!!! Oh Save Player God");
                }

            }
            if (other.TryGetComponent(out IMakeDamage damage))
            {
                Take(damage.Get(attributes));
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"[AI] losing in range:[{other.name}]");
            if (other.CompareTag(controller.target))
            {
                if (haveAim)
                {
                    haveAim = false;
                    controller.Move(this);
                }
            }
        }

        public void Take(Attributes attributes)
        {
            controller.Take(attributes, this);
        }


        public int GetPoints()
        {
            return model.Points;
        }
        public override Attributes GetModelAttributes()
        {
            return model.Attributes;
        }
    }
}