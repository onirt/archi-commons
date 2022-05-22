using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Data/Models/Weapon")]
    public class WeaponModel : PawnModel, IMakeDamage
    {
        [SerializeField] protected SpawnData spawner;
        [SerializeField] private float multiplier = 0.1f;

        public SpawnData Spawner { get => spawner; }

        public virtual Attributes Get(Attributes _attributes)
        {
            Attributes newAttribute = new Attributes(attributes);
            newAttribute.attack += _attributes.attack * 0.1f;
            newAttribute.dextery += _attributes.dextery * 0.1f;
            newAttribute.speed += _attributes.speed * 0.1f;

            return newAttribute;
        }
        public void Instantiate(Transform parent)
        {
            spawner.Instantiate(addressable, parent, SpawnMode.Parent, 1, Spawned);
        }
        void Spawned(GameObject spawned)
        {
            Debug.Log($"[Weapon] spawned succes: [{spawned.name}]");
        }
    }
}