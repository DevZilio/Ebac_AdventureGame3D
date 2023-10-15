using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;
    public List<CheckPointBase> checkPoints;



    private void Start()
    {
        lastCheckPointKey = SaveManager.Instance.Setup.lastChekPoint;
    }

    public bool HasCheckPoint()
    {
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
            Debug.Log("Checkpoint " + lastCheckPointKey + " saved");
        }
    }

   public Vector3 GetPositionFromLastCheckPoint()
{
    if (lastCheckPointKey > 0 && checkPoints != null && checkPoints.Count > 0)
    {
        var checkPoint = checkPoints.Find(i => i.key == lastCheckPointKey);

        if (checkPoint != null)
        {
            Debug.Log("Loaded position from checkpoint " + lastCheckPointKey);
            return checkPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("Could not find a checkpoint with key " + lastCheckPointKey);
        }
    }
    else
    {
        Debug.LogWarning("No valid checkpoints or lastCheckPointKey is zero.");
    }

    // Retorne uma posição padrão ou qualquer posição adequada em caso de erro.
    return Vector3.zero;
}

}
 