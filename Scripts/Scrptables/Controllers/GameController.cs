using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameController : ScriptableObject
{
    [SerializeField] private GameSetup setup;

    public GameSetup Setup { get => setup; }

    public abstract void GameStart();
    public abstract void GameEnded();


}
