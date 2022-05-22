using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi.Controllers
{
    public abstract class AIController<T> : ScriptableObject, IMove<T>
    {
        //public abstract void Attack(AIBehaviour behvioaur);
        //public abstract void Evade();

        public abstract void Move(T kinematic);
    }
}
