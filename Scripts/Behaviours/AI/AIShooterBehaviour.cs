using ArChi.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace ArChi
{
    public class AIShooterBehaviour : AICharacterBehaviour,
        ITakeDamage,
        IController<AIShooterController>
    {
        [Header("Channels")]
        [SerializeField] private AIBehaviourEventChannel iDiedChannel;
        [Space(20)]
        [Header("Controllers")]
        public AIShooterController controller;
        [Space(20)]
        [SerializeField] protected SphereCollider detector;
        [SerializeField] protected Transform uiSpot;
        [SerializeField] protected Transform weaponSpot;


        protected CharacterModel _model;
        public CharacterModel _Model { get => _model; }
        protected int weaponSelected;
        public float coldown;

        public struct Aim
        {
            public Transform target;
            public ITakeDamage damage;
        }

        protected Aim targetAim;

        public bool haveAim;

        public Aim TargetAim { get => targetAim; }

        public AIBehaviourEventChannel DiedChannel { get => iDiedChannel; }

        public int WeaponSelected { get => weaponSelected; }

        public override void Init()
        {
            base.Init();
            _model = (CharacterModel)GetModel();
            weaponSelected = UnityEngine.Random.Range(0, _model.WeaponsCount);
            _model.InstantiateUI(1, uiSpot, SetUI);
            _model.InstantiateWeapon(weaponSelected, weaponSpot);
            detector.radius = _model.GetWeaponRange(weaponSelected);
            GetController().SetPose(0, this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GetController().target))
            {
                Debug.Log($"[CH1][Trigger][{name}] i detected my target: {other.name}");
                ITakeDamage take = other.GetComponentInParent<ITakeDamage>();
                if (take != null)
                {
                    if (!haveAim)
                    {
                        haveAim = true;
                        targetAim = new Aim { target = other.transform, damage = take };
                    }
                }
                if (detector.radius > _model.GetWeaponRange(weaponSelected))
                {
                    state = CharacterState.Searching;
                }
                else
                {
                    state = CharacterState.Attacking;
                }
            }
            if (other.TryGetComponent(out IMakeDamage damage))
            {
                Take(damage.Get(attributes));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GetController().target))
            {
                Debug.Log($"[CH1][Trigger][{name}] mytrigger exit: {other.name}");
                if (haveAim)
                {
                    haveAim = false;
                }
                state = CharacterState.Seeking;
            }
        }
        private void SetUI(GameObject gameObject)
        {

        }

        private void SeekExhaustion()
        {
            detector.radius = GetRange();
        }

        public float GetRange()
        {
            return _model.GetWeaponRange(weaponSelected);
        }

        public void Take(Attributes attributes)
        {
            //GetController().Take(attributes, this);
        }

        public AIShooterController GetController()
        {
            return controller;
        }
    }
}
