using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public StateMachine<GameState> GameStates;

    private void Start()
    {
        GameStates = new StateMachine<GameState>(new DefaultGameState());

        SceneManager.LoadScene(m_startScene.ScenePath);
    }

    [SerializeField]
    private ScenePicker m_startScene;
}


public class DefaultGameState : GameState
{
    public override bool CanTransitionTo(State requestedNewState)
    {
        return true;
    }

    public override void OnEnter(State previousState)
    {
    }

    public override void OnExit(State nextState)
    {
    }

    public override void Tick(float deltaTime)
    {
    }
}