using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class CharacterModel : PawnModel
    {
        [SerializeField] protected WeaponModel[] weapons;
        [SerializeField] protected float stamina;

        public int WeaponsCount { get => weapons.Length; }

        public float Stamina { get => stamina; }

        public IMakeDamage GetWeaponDamage(int i)
        {
            return weapons[i];
        }
        public float GetWeaponRange(int i)
        {
            return weapons[i].Attributes.range;
        }

        public void InstantiateWeapon(int i, Transform parent)
        {
            weapons[i].Instantiate(parent);
        }
    }
}
