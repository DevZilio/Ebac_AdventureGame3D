using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
   public MeshRenderer meshRenderer;
   public SkinnedMeshRenderer skinnedMeshRenderer;

   public string colorParameter = "_EmissionColor";



   private Tween _currTween;

   [Header("Setup")]
   public Color color = Color.red;
   public float duration = .1f;


   private void OnValidate() {
       if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
       if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
   }

   [NaughtyAttributes.Button]
   public void Flash()
   {
       if(meshRenderer != null && !_currTween.IsActive())
       _currTween = meshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
       if(skinnedMeshRenderer != null && !_currTween.IsActive())
       _currTween = skinnedMeshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
       Debug.Log("Flash");
   }
}
