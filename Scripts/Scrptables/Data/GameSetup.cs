using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "GameSetup", menuName = "Data/Game/Base")]
    public class GameSetup : ScriptableObject
    {
        public GameType type;
        public float GameTime;
        public float Rounds;
        public int PlayerNumbers;
    }
    public enum GameType
    {
        Survivor,
        Race
    }
}