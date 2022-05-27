using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArChi
{
    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Data/Models/Weapon")]
    public class WeaponModel : PawnModel, IMakeDamage
    {
        [SerializeField] protected SpawnData fxSpawner;
        [SerializeField] private float multiplier = 0.1f;
        [SerializeField] protected int spotChildIndex;
        protected int fxIndex;

        public SpawnData FXSpawner { get => fxSpawner; }
        public int FXIndex { get => fxIndex; set => fxIndex = value; }

        public virtual Attributes MakeDamage(Attributes _attributes)
        {
            Attributes newAttribute = new Attributes(attributes);
            newAttribute.attack += _attributes.attack * 0.1f;
            newAttribute.dextery += _attributes.dextery * 0.1f;
            newAttribute.speed += _attributes.speed * 0.1f;

            return newAttribute;
        }
        public void Instantiate(Transform parent, UnityAction<GameObject> action)
        {
            action += Spawned;
            spawner.Instantiate(index, parent, SpawnMode.Parent, 1, action);
        }
        private void Spawned(GameObject spawned)
        {
            Debug.Log($"[Weapon] spawned succes: [{spawned.name}]");

            fxSpawner.Instantiate(fxIndex, spawned.transform.GetChild(spotChildIndex), SpawnMode.Parent, 1, BulletSpawned);
        }
        private void BulletSpawned(GameObject spawned)
        {
            ParticleDamageBehaviour particleDamage = spawned.GetComponentInChildren<ParticleDamageBehaviour>();
            if (particleDamage)
            {
                particleDamage.Damage = this;
                IShooter shooter = spawned.GetComponentInParent<IShooter>();
                shooter.SetShoot(particleDamage);
            }
        }
    }
}