using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button yourButton; // Referência ao seu botão
    public KeyCode keyToPress; // A tecla que ativa o botão

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            yourButton.onClick.Invoke(); // Ativa o evento de clique do botão
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
