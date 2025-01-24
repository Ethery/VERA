using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GameManager<GameManager>
{

}

public abstract class GameManager<T> : Singleton<T> where T : MonoBehaviour
{
    public StateMachine<GameState> GameStates;

    public string StartScene => m_startScene.ScenePath;

    public static bool IsReady => m_isReady;

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
	private static bool m_isReady = false;

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
