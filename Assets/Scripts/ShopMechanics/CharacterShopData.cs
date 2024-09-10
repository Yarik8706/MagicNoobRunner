using UnityEngine;



namespace ShopMechanics
{
    [CreateAssetMenu(fileName = ("CharactershopOB"),menuName = ("Shopping/Character Shop"),order = 1)]
    public class CharacterShopData : ScriptableObject
    {
        public Character[] characters;

        public Character GetCharacter(int index) => characters[index];
    }
}
