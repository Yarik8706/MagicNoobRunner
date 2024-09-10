using blocks;
using UI;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class SomethingForAdsButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _afterAds;

    private static string _rewardedName;

    private void OnEnable() => YandexGame.RewardVideoEvent += SomethingForAds;

    private void OnDisable() => YandexGame.RewardVideoEvent -= SomethingForAds;

    public void StartRewardedVideo()
    {
        Debug.Log("Start");
        _rewardedName = gameObject.name;
        YandexGame.RewVideoShow((int)VideoAdsId.RewardForAds);
    }

    private void SomethingForAds(int value)
    {
        Debug.Log("Reward");
        if (value == (int)VideoAdsId.RewardForAds && _rewardedName == gameObject.name)
        {
            _afterAds.Invoke();
        }
    }

    public void ThreeXLevelIncomeReward()
    {
        Debug.Log("Income");
        var reward = CoinManager.Instance.LevelsIncome * 2;
        CoinManager.Instance.AddCoins(reward);
        CoinManager.Instance.LevelsIncome += reward;
        GameEvents.UpdateMoneyCount.Invoke();
    }

    public void AddMoney(int amount)
    {
        CoinManager.Instance.AddCoins(amount);
        GameEvents.UpdateMoneyCount.Invoke();
    }

    public void SkipLevel()
    {
        GameDataManager.AddLevel(1);
        GameEvents.ResetLevelEvent.Invoke();
    }
}