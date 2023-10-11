using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Level1 : MonoBehaviour
{
    public Transform playerStartPosition;
    public SaveManager saveManager;
    public GameObject playerPrefab;
    private GameObject _player;

    [Header("Animation")]
    public float duration = .5f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        // Verifique se o SaveManager existe e se há um jogo salvo
        if (saveManager != null)
        {
            // Restaure as informações do jogo salvo
            int savedCoins = saveManager.GetSavedCoins();
            int savedLifePacks = saveManager.GetSavedLifePacks();


            // Aplique as informações restauradas ao jogador ou a outros objetos, se necessário
            // Exemplo: player.Coins = savedCoins;

            Debug.Log("Informações do jogo salvo restauradas. Moedas: " + savedCoins + ", Life Packs: " + savedLifePacks);
        }

        Init();

    }

    private void Init()
    
    {
        SpawnPlayer();
        
    }

    private void SpawnPlayer()
{
    // Posicione o jogador na posição inicial
    if (playerStartPosition != null)
    {
        if (playerPrefab != null)
        {   
            _player = Instantiate(playerPrefab);
            _player.transform.position = playerStartPosition.transform.position;
            _player.transform.DOScale(0, duration).SetEase(ease).From();
        }
        else
        {
            Debug.LogWarning("Prefab do jogador não atribuído.");
        }
    }
    else
    {
        Debug.LogWarning("Posição inicial do jogador não definida.");
    }
}

}
