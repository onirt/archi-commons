using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IUICharacter
    {
        void SetMaxHealth(float value);
        void UpdatedHealth(float health);
    }
}
