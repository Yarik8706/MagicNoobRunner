using System;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

namespace ShopMechanics
{
    public class ShopItemsGenerator : MonoBehaviour
    {
        public List<CharacterItem> Items { get; } = new();
        public CharacterShopData characterShopData;

        [SerializeField] private int countInScreen;
        [SerializeField] private GameObject screenPrefab;
        [SerializeField] private CharacterItem _characterItem;
        [SerializeField] private Transform _parentSpawn;
        [SerializeField] private SimpleScrollSnap _simpleScrollSnap;
        [SerializeField] private Canvas canvas;

        public void Init()
        {
            var screenObjectCount = 0;
            var activeScreen = Instantiate(screenPrefab, _parentSpawn);
            for (var i = 0; i < characterShopData.characters.Length; i++)
            {
                if (screenObjectCount == countInScreen)
                {
                    activeScreen = Instantiate(screenPrefab, _parentSpawn);
                    screenObjectCount = 0;
                }

                screenObjectCount++;
                var characterItem = Instantiate(_characterItem, activeScreen.transform);
                characterItem.SetCharacter(characterShopData.characters[i], i);
                Items.Add(characterItem);
            }

            _simpleScrollSnap.Setup();
        }

        private void OnEnable()
        {
            _simpleScrollSnap.Setup();
        }
    }
}