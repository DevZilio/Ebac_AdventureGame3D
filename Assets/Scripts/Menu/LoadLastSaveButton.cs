using Items;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastSaveButton : MonoBehaviour
{

   

    public void LoadLastSave()
    {
        // Carrega os dados do último ponto de salvamento
        SaveManager.Instance.LoadFile();
        ItemManager.Instance.LoadItemsFromSave();

        // Obtém as informações do último ponto de salvamento
        int lastLevel = SaveManager.Instance.Setup.lastLevel;
        int lastCheckpoint = SaveManager.Instance.Setup.lastChekPoint;

        if (lastLevel == 0)
    {
        Debug.Log("lastLevel é igual a 0. A função será interrompida.");
        return;
    }

        // Carrega a cena do último nível (certifique-se de que a cena tenha o mesmo índice do número do nível)
        SceneManager.LoadScene(lastLevel);

         GameManager.Instance.stateMachine.SwitchState(GameManager.GameStates.GAMEPLAY);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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
