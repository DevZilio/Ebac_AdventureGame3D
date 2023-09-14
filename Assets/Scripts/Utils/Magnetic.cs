using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float distance = .2f;
    public float coinSpeed = 3f;
   
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.position)>distance)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
        
    }
}
