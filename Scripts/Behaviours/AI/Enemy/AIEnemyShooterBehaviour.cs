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

        public float PerceptionRadius { set => detector.radius = value; get => detector.radius; }
        public override void Init()
        {
            base.Init();
            _controller = (AIEnemyShooterController)controller;
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
            return ((EnemyModel)model).Points;
        }
        public override PawnModel GetModel()
        {
            return model;
        }
    }
}