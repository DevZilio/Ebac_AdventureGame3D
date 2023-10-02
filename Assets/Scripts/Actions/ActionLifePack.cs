using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.C;
    public SOInt soInt;

    [Header("Audio")]
    public SFXType sfxType;

    private void Start() {
       soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            // Debug.Log("RecoverLife");
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            PlayerLife.Instance.ResetLife();
            Play();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(keyCode))
        {
            // Debug.Log("Key L");
            RecoverLife();
        }
    
    }

     private void Play()
    {
        SFXPool.Instance.Play(sfxType);
    }
}
