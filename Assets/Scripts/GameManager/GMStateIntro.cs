using DevZilio.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMStateIntro : StateBase
{
    public string menuSceneName = "SCN_Menu"; // Nome da cena do menu

    public override void OnStateEnter(params object[] objs)
    {
        // Carregue a cena do menu quando o estado de introdução for ativado
        SceneManager.LoadScene(menuSceneName);
    }
}