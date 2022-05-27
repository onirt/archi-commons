using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class WeaponBaseBehaviour : MonoBehaviour, IReferences
    {
        [SerializeField] private Transform[] handReferences;

        public Transform GetReference(int i)
        {
            if (i < handReferences.Length)
            {
                return handReferences[i];
            }
            Debug.LogError($"[References] Index Out of Range [{i}, {handReferences.Length}]");
            return null;
        }
    }
}
