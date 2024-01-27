using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Joining, Game, End, None };

    public event System.Action<State> GameStateChangedEvent;

    public State CurrentState 
    { 
        get => gameState;
        private set
        {
            if (gameState != value)
            {
                GameStateChangedEvent?.Invoke(value);
            }
            gameState = value;
        }
    }

    private State gameState = State.None;

    private void Start()
    {
        CurrentState = State.Joining;
    }

    public void EndState(State state)
    {
        if (CurrentState != state)
        {
            return;
        }

        switch (state)
        {
            case State.Joining:
                CurrentState = State.Game;
                break;
            case State.Game:
                CurrentState = State.End;
                break;
            case State.End or State.None:
                break;
        }
    }
}
