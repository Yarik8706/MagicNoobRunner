using System;
using UnityEngine;

namespace ShopMechanics
{
    public class ShopSkinControl : MonoBehaviour
    {
        [SerializeField] private Texture[] playerHeroSkinTextures;
        [SerializeField] private Material playerHeroSkinMaterial;

        public void Init()
        {
            ChangeSkin(0);
        }

        public void ChangeSkin(int index)
        {
            playerHeroSkinMaterial.SetTexture("_MainTex", playerHeroSkinTextures[index]);
        }
    }
}