using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "CharacterRigPose", menuName = "Pose/Character")]
    public class CharacterRigPose : ScriptableObject
    {
        [SerializeField] private Vector3 headTargetPosition;
        [SerializeField] private Vector3 headTargetRotation;

        [SerializeField] private Vector3 rightTargetPosition;
        [SerializeField] private Vector3 rightTargetRotation;
        [SerializeField] private Vector3 leftTargetPosition;
        [SerializeField] private Vector3 leftTargetRotation;

        public Vector3 HeadTargetPosition { get => headTargetPosition; }
        public Vector3 HeadTargetRotation { get => headTargetRotation; }
        public Vector3 RightTargetPosition { get => rightTargetPosition; }
        public Vector3 RightTargetRotation { get => rightTargetRotation; }
        public Vector3 LeftTargetPosition { get => leftTargetPosition; }
        public Vector3 LeftTargetRotation { get => leftTargetRotation; }
    }
}
