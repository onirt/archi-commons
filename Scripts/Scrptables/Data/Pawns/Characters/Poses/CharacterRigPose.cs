using UnityEngine;
using System;

namespace ArChi
{
    [CreateAssetMenu(fileName = "CharacterRigPose", menuName = "Pose/Character")]
    public class CharacterRigPose : ScriptableObject
    {
        [SerializeField] private Pose[] poses;

        public void Set(IPoseCharacter icharacter)
        {
            for (int i=0; i < poses.Length; i++)
            {
                SetValues(poses[i], icharacter);
            }
        }
        private void SetValues(Pose pose, IPoseCharacter icharacter)
        {
            switch (pose.reference)
            {
                case PoseReference.Head:
                    icharacter.SetHead(pose.transform);
                    break;
                case PoseReference.Chest:
                    icharacter.SetChest(pose.transform);
                    break;
                case PoseReference.RightHand:
                    icharacter.SetRightHand(pose.transform);
                    break;
                case PoseReference.Lefthand:
                    icharacter.SetLeftHand(pose.transform);
                    break;
                case PoseReference.Weapon:
                    icharacter.SetWeapon(pose.transform);
                    break;
            }
        }
    }
}
