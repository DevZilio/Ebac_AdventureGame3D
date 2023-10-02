using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastSaveButton : MonoBehaviour
{
  public void LoadLastSave()
{
    // Carrega os dados do último ponto de salvamento
    SaveManager.Instance.LoadFile();

    // Obtém as informações do último ponto de salvamento
    int lastLevel = SaveManager.Instance.Setup.lastLevel;
    int lastCheckpoint = SaveManager.Instance.Setup.lastChekPoint;

    // Carrega a cena do último nível (certifique-se de que a cena tenha o mesmo índice do número do nível)
    SceneManager.LoadScene(lastLevel);

    // Encontra o objeto do jogador (Player) na cena atual
    PlayerLife player = FindObjectOfType<PlayerLife>();

    if (player != null)
    {
        // Defina a posição do jogador com base no último checkpoint
        player.Respawn();
    }
    else
    {
        Debug.LogWarning("Player not found in the scene.");
    }
}

}
  