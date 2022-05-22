using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IMove<T>
    {
        void Move(T kinematic);
    }
}
