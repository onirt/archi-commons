using ArChi.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace ArChi
{
    public class AIEnemyShooterBehaviour : AIShooterBehaviour, IScore
    {
        [Header("Controllers")]
        private AIEnemyShooterController _controller;

        public float exhaustionColdown;

        private Coroutine moving;

        public float PerceptionRadius { set => detector.radius = value; get => detector.radius; }
        public override void Init()
        {
            base.Init();
            _controller = (AIEnemyShooterController)controller;
            if (moving == null)
            {
                moving = StartCoroutine(Move());
            }
        }
        IEnumerator Move()
        {
            while(true){

                transform.position = Vector3.zero;
                yield return null;
            }
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
                    Debug.Log($"[CH1][{name}] my: {state}");
                    _controller.Attack(targetAim.damage, this);
                    break;
                case CharacterState.Searching:
                    Debug.Log($"[CH1][{name}] my: {state}");
                    _controller.Search(this);
                    break;
                case CharacterState.Seeking:
                    _controller.Seek(this);
                    break;
                case CharacterState.Patrol:
                    Debug.Log($"[CH1][{name}] my: {state}");
                    _controller.Patrol(this);
                    break;
                default:
                    if (isMoving)
                    {
                        _controller.Move(this);
                    }
                    else
                    {
                        _controller.Think(this);
                    }
                    break;
            }
        }

        public int GetPoints()
        {
            if (moving != null)
            {
                StopCoroutine(moving);
            }
            return ((EnemyModel)model).Points;
        }
        public override PawnModel GetModel()
        {
            return model;
        }
    }
}