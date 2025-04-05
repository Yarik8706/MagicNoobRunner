using blocks;
using ShopMechanics;
using System;
using System.Collections;
using UI;
using UnityEngine;
using YG;

namespace Flatformer.GameData
{
    public class YandexControl : MonoBehaviour
    {
#if UNITY_EDITOR 
        [SerializeField] private bool _needResetData = true;
#endif
        private void Awake()
        {
            StartCoroutine(YandexSDKEnabledCoroutine());
        }

        private IEnumerator YandexSDKEnabledCoroutine()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled);
#if UNITY_EDITOR 
            if(_needResetData) YandexGame.ResetSaveProgress();
#endif
            MultiTextUI.lang = YandexGame.lang;
            MultiTextUI.ChangeLanguageEvent.Invoke();
            YandexGame.InitEnvirData();
            GameDataManager.InitData();
            // ShopManager.Instance.Init();
            InitMusicState.Instance.Init();
            GameManager.Instance.StartGame();
            CoinManager.Instance.UpdateTexts();
            EveryDayRewardUI.Instance.Init();
            // GameplayShop.Instance.Init();
#if UNITY_EDITOR
            var playerData = GameDataManager.GetPlayerData();
            Debug.Log("playerData.freeSpinsCount: " + playerData.freeSpinsCount);
#endif
            GameManager.Instance.Play();
        }

        [ContextMenu("AddFreeSpins")]
        public void AddFreeSpins()
        {
            GameDataManager.AddFreeSpin(1);
        }
    }
}