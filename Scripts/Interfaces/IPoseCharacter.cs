using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IPoseCharacter
    {
        void SetHead(SimpleTransform transform);
        void SetChest(SimpleTransform transform);
        void SetRightHand(SimpleTransform transform);
        void SetLeftHand(SimpleTransform transform);
        void SetWeapon(SimpleTransform transform);

    }
}
