using UnityEngine;

namespace ShopMechanics
{
    [System.Serializable]
    public struct Character
    {
        public Sprite image;
        public int price;
        public bool isNeedAds;
        public int levelRequired;
        public int entryDaysRequired;
    }
}