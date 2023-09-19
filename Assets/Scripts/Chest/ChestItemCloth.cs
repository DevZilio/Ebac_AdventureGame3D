using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cloth;

public class ChestItemCloth : ChestItemBase
{
   
    public GameObject clothItem;
   
    public float tweenEndTime = .5f;
   

    public override void ShowItem()
    {
        base.ShowItem();

        CreateItems();
    }

[NaughtyAttributes.Button]
    private void CreateItems()
    {
       
            var item = Instantiate(clothItem);
            item.transform.position = transform.position;
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            Debug.Log("CreateItems");
        
    }

[NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();

            var i = clothItem;
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime/2).SetDelay(tweenEndTime/2);
            ClothItemStrong.Instance.Collect();
        
    }
}
