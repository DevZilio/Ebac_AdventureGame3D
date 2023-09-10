using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 1;

    private bool checkPointActived = false;
    private string _checkPointKey = "CheckPointKey";

    private void OnTriggerEnter(Collider collider)
    {
        if(!checkPointActived && collider.transform.tag == "Player")
        {

        CheckCheckPoint();
        Debug.Log("CheckPoint");
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
        meshRenderer.material.SetColor("_EmissionColor", Color.white);

    }
[NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);

    }

    private void SaveCheckPoint()
    {
        // if(PlayerPrefs.GetInt(_checkPointKey, 0)>key)
        //     PlayerPrefs.SetInt(_checkPointKey, key);

        CheckPointManager.Instance.SaveCheckPoint(key);
        
        checkPointActived = true;
    }
}
