using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    private Button newGameButton;

    public SaveManager saveManager; // Atribua o SaveManager a este campo no Inspector
    public string firstLevelSceneName = "SCN_Art_3D_v2"; // Substitua pelo nome da cena do Level 1

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
        // Faça a transição para o estado GAMEPLAY
        GameManager.Instance.stateMachine.SwitchState(GameManager.GameStates.GAMEPLAY);

        
        // Crie um novo save
        saveManager.CreateNewSave();

        // Carregue a cena do primeiro nível (ou o nível inicial do seu jogo)
        SceneManager.LoadScene(firstLevelSceneName);
    }
}
