using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArChi
{
    public class CharacterModel : PawnModel
    {
        [SerializeField] protected SpawnData ui;
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
        public void InstantiateUI(int level, Transform parent, UnityAction<GameObject> response)
        {
            ui.Instantiate(parent, level, response);
        }
    }
}
