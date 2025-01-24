using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeraGameManager : GameManager<VeraGameManager>
{
    public GameDatas GameDatas => m_gameDatas;

    [SerializeField]
    private GameDatas m_gameDatas;
}
