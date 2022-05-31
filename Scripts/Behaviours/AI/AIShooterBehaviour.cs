using ArChi.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

namespace ArChi
{
    public class AIShooterBehaviour : AICharacterBehaviour,
        ITakeDamage,
        IShooter,
        IShoot,
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
        public Transform Weapon { get => weaponSpot; }

        protected int weaponSelected;
        public float coldown;

        public UnityEvent<IMakeDamage> shootEvent;
        public IUICharacter ui;

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

        private void OnDestroy()
        {
            shootEvent.RemoveAllListeners();
        }
        public override void Init()
        {
            base.Init();
            _model = (CharacterModel)GetModel();
            weaponSelected = UnityEngine.Random.Range(0, _model.WeaponsCount);
            _model.InstantiateUI(1, uiSpot, SetUI);
            _model.InstantiateWeapon(weaponSelected, weaponSpot, SetReferences);
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
            /*if (other.TryGetComponent(out IMakeDamage damage))
            {
                Take(damage.MakeDamage(attributes));
            }*/
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
        private void SetReferences(GameObject spawnedReferences)
        {
            if (!rightControlReference)
            {
                Debug.LogError($"[References] Right controller reference in {name} is null");
                return;
            }
            if (!leftControlReference)
            {
                Debug.LogError($"[References] left controller reference in {name} is null");
                return;
            }
            IReferences references = spawnedReferences.GetComponentInChildren<IReferences>();
            if (references != null)
            {
                if (references.GetReference(0) != null)
                    SetReference(references.GetReference(0), rightControlReference);
                else
                    Debug.LogError("[References] Reference 0 is null");

                if (references.GetReference(1) != null)
                    SetReference(references.GetReference(1), leftControlReference);
                else
                    Debug.LogError("[References] Reference 1 is null");
            }
            else
            {
                Debug.Log("[References] No references found");
            }
        }
        private void SetReference(Transform reference, Transform obj)
        {
            //obj.SetParent(reference);
            //obj.position = reference.position;
            //obj.rotation = reference.rotation;
            obj.localPosition = Vector3.zero;
            obj.localRotation = Quaternion.identity;
        }
        private void SeekExhaustion()
        {
            detector.radius = GetRange();
        }

        public float GetRange()
        {
            return _model.GetWeaponRange(weaponSelected);
        }
        public IMakeDamage GetWeaponDamage()
        {
            return _model.GetWeaponDamage(WeaponSelected);
        }
        public void SetShoot(IShoot shoot)
        {
            shootEvent.AddListener(shoot.Shoot);
        }
        public void Shoot(IMakeDamage damage)
        {
            shootEvent?.Invoke(damage);
        }
        public void Take(Attributes attributes)
        {
            Debug.Log($"[Shoot][Damage] taking damage: {attributes.attack} [{name}]");
            GetController().Take(attributes, this);
        }

        public AIShooterController GetController()
        {
            return controller;
        }
        private void SetUI(GameObject uiGameObject)
        {
            Debug.Log($"[Attributes][SetUI][{name}] Attributes: {attributes.health}");
            if (uiGameObject.TryGetComponent(out IUICharacter character))
            {
                character.SetMaxHealth(attributes.health);
                ui = character;
            }
            controller.Connector.TriggerEvent(uiGameObject);
        }
        public override void SetWeapon(SimpleTransform transform)
        {
            if (!weaponSpot) return;
            weaponSpot.localPosition = transform.position;
            weaponSpot.localRotation = Quaternion.Euler(transform.rotation);
        }
    }
}
