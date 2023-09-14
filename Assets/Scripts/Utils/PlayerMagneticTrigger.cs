using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{

  private void OnTriggerEnter(Collider ohter)
  {
      ItemCollectableBase i = ohter.transform.GetComponent<ItemCollectableBase>();
      if(i!=null)
      {
          i.gameObject.AddComponent<Magnetic>();
      }
  }
}
