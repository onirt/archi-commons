using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "PawnModel", menuName = "Data/Models/Pawn")]
    public class PawnModel : ScriptableObject
    {
        [SerializeField] protected SpawnData spawner;
        [SerializeField] protected Attributes attributes;
        protected int index;

        public SpawnData Spawner { get => spawner; }
        public Attributes Attributes { get => attributes; }
        public int Index { get => index; set => index = value; }
    }
}