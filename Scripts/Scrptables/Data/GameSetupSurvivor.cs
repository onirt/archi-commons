using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "GameSetupSurvivor", menuName = "Data/Game/Survivor")]
    public class GameSetupSurvivor : GameSetup
    {
        public int[] goals;

        public int GetTarget(int round)
        {
            return goals[round];
        }
    }
}