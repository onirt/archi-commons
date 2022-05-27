using ArChi.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIPlayerShooterBehaviour : AIShooterBehaviour, IDestination, IAttack
    {
        private AIShooterController _controller;
        public override void Init()
        {
            base.Init();
            _controller = (AIShooterController)controller;
        }
        private void Update()
        {
            if (_controller.stay)
            {
                return;
            }
            switch (state)
            {
                case CharacterState.Attacking:
                    _controller.Attack(targetAim.damage, this);
                    break;
                default:
                    if (isMoving)
                    {
                        _controller.Move(this);
                    }
                    break;
            }
        }
        public void SetDestination(Vector3 destination)
        {
            Debug.Log($"[Game][Touch] destination listened: {destination}");
            agent.SetDestination(destination);
            state = CharacterState.Searching;
        }

        public void Attack(Transform target)
        {
            ITakeDamage take = target.GetComponentInParent<ITakeDamage>();
            if (take != null)
            {
                haveAim = true;
                targetAim = new Aim { target = target, damage = take };
                state = CharacterState.Attacking;
            }
        }
    }
}
