using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : ClothItemBase
    {
        public List<SkinnedMeshRenderer> meshes;
        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;


        private void Awake() {
             foreach (var meshRenderer in meshes)
            {
                if (meshRenderer.materials.Length > 0)
                {
                    _defaultTexture = (Texture2D) meshRenderer.materials[0].GetTexture(shaderIdName);
                }
            }
            
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            foreach (var meshRenderer in meshes)
            {
                if (meshRenderer.materials.Length > 0)
                {
                    meshRenderer.materials[0].SetTexture(shaderIdName, texture);
                }
            }
        }

        [NaughtyAttributes.Button]
        public void ChangeTexture(ClothSetup setup, float duration)
        {
            foreach (var meshRenderer in meshes)
            {
                if (meshRenderer.materials.Length > 0)
                {
                    meshRenderer.materials[0].SetTexture(shaderIdName, setup.texture);
                }
            }
        }


        [NaughtyAttributes.Button]
        public void ResetTexture()
        {
              foreach (var meshRenderer in meshes)
            {
                if (meshRenderer.materials.Length > 0)
                {
                    meshRenderer.materials[0].SetTexture(shaderIdName, _defaultTexture);
                }
            }
        }
    }
}