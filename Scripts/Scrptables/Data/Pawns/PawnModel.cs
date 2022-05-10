using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "PawnModel", menuName = "Data/Models/Pawn")]
    public class PawnModel : ScriptableObject
    {
        [SerializeField] protected string addressable;
        [SerializeField] protected Attributes attributes;

        public Attributes Attributes { get => attributes; }
    }
}