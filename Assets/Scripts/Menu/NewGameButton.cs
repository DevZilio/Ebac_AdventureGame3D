using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    private Button newGameButton;

    public int level1 = 1;
    
    public SaveManager saveManager; // Atribua o SaveManager a este campo no Inspector
    public Transform playerStartPosition; // Atribua o objeto playerStartPosition a este campo no Inspector

    private void Awake()
    {
        newGameButton = GetComponent<Button>();

        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(StartNewGame);
        }
        else
        {
            Debug.LogError("NewGameButton component not found.");
        }
    }

    public void StartNewGame()
    {
        // Configura a posição inicial do jogador no SaveManager
        saveManager.playerStartPosition = playerStartPosition.position; // Use a posição do objeto playerStartPosition como posição inicial

        // Crie um novo save
        saveManager.CreateNewSave();

        // Carregue a cena do primeiro nível (ou o nível inicial do seu jogo)
        int firstLevel = 1; // Altere isso para corresponder ao número do primeiro nível
        SceneManager.LoadScene(firstLevel);
    }
}
