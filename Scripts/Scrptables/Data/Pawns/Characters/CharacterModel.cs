using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class CharacterModel : PawnModel
    {
        [SerializeField] protected CharacterRigPose[] poses;
        [SerializeField] protected SpawnData spawner;
        [SerializeField] protected WeaponModel[] weapons;

        public int WeaponsCount { get => weapons.Length; }
        public SpawnData Spawner { get => spawner; }

        public IMakeDamage GetWeapon(int i)
        {
            return weapons[i];
        }

        public void InstantiateWeapon(int i, Transform parent)
        {
            weapons[i].Instantiate(parent);
        }
    }
}
