using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Data/Models/Enemy")]
    public class EnemyModel : CharacterModel
    {

        [SerializeField] private int points;

        public int Points { get => points; }

    }
}