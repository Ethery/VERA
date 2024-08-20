using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public StateMachine<GameState> GameStates;
    public string StartScene => m_startScene.ScenePath;

    public bool IsReady => m_isReady;

	protected override void Awake()
	{
        base.Awake();
        m_isReady = false;
	}

	private void Start()
    {
        GameStates = new StateMachine<GameState>(new DefaultGameState());
        m_isReady = true;
    }

    [NonSerialized]
    private bool m_isReady = false;

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