using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using DevZilio.StateMachine;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;
    public Player player;

     protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
         LoadPlayerPositionFromLastCheckpoint();
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    public void InitGame()
    {

    }


  public void LoadPlayerPositionFromLastCheckpoint()
    {
        if (SaveManager.Instance != null && player != null)
        {
            if (SaveManager.Instance.Setup != null)
            {
                // Carregue a última posição salva do jogador a partir do CheckpointManager
                // player.transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            }
            else
            {
                Debug.LogWarning("SaveManager Setup is null. Cannot load player position.");

                // Se o SaveManager não tiver dados, use a posição inicial do SaveManager
                player.transform.position = SaveManager.Instance.playerStartPosition;
            }
        }
        else
        {
            Debug.LogWarning("SaveManager or player is null. Cannot load player position.");
        }
    }

    // ...
}





 