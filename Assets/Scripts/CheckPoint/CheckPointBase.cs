using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 1;

    private bool checkPointActived = false;
    private string _checkPointKey = "CheckPointKey";

    [Header("Audio")]
    public SFXType sfxType;

    private void OnTriggerEnter(Collider collider)
    {
        if(!checkPointActived && collider.transform.tag == "Player")
        {

        CheckCheckPoint();
        // Debug.Log("CheckPoint");
        }
    }

    private void CheckCheckPoint()
    {
        TurnItOn();
        SaveCheckPoint();
    }

[NaughtyAttributes.Button]
    private void TurnItOn()
    {
        Play();
        meshRenderer.material.SetColor("_EmissionColor", Color.white);

    }
[NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);

    }

    private void SaveCheckPoint()
{
    CheckPointManager.Instance.SaveCheckPoint(key);

    // Salva as informações no SaveManager
    SaveManager.Instance.SaveItems(); // Salva moedas e saúde
    SaveManager.Instance.SaveLastCheckPoint(); // Salva o último checkpoint

    // Atualiza o número do nível atual
    int currentLevel = SceneManager.GetActiveScene().buildIndex;
    SaveManager.Instance.SaveLastLevel(currentLevel); // Salva o último nível

    // Chama o método Save() para salvar todas as informações
    SaveManager.Instance.Save();

    Debug.Log("Save game on checkpoint");
}


    private void Play()
    {
        SFXPool.Instance.Play(sfxType);
    }
}
