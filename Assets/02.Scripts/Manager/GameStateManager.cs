using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    NEWGAME,
    LOADGAME
}

public class GameStateManager : MonoBehaviour
{
    private GameState _currentGameState;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

}
