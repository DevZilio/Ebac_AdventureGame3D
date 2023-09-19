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

        // Encontra o objeto do jogador (Player) e chama a função Respawn para posicionar o jogador no último checkpoint
        PlayerLife  player = FindObjectOfType<PlayerLife>();
        if (player != null)
        {
            player.Respawn();
        }
    }
}
