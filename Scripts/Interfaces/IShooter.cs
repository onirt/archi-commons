using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IShooter
    {
        float GetRange();
        IMakeDamage GetWeaponDamage();
        void SetShoot(IShoot shoot);
    }
}
