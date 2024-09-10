using blocks;
using System;
using ShopMechanics;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;

public class EveryDayRewardUI : MonoBehaviour
{
    [SerializeField] private int _freeSkinIndex;
    [SerializeField] private GameObject _freeSkinRewardWindow;
    [SerializeField] private GameObject _freeSkinRewardItem;
    
    [SerializeField] private TMP_Text _freeMoneyWindowText;
    [SerializeField] private GameObject _freeMoneyWindow;

    [SerializeField] private TMP_Text _freeSpinsRewardWindowText;
    [SerializeField] private GameObject _freeSpinsRewardWindow;

    [SerializeField] private UnityEvent[] _rewardEvents;
    [SerializeField] private EveryDayRewardItem[] _items;
 
    public static EveryDayRewardUI Instance { get; private set; }

    private PlayerData _playerData;

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        _playerData.lastCallDate = DateTime.Now.DayOfYear-1;
        Init();
    }

    public void Init()
    {
        _playerData = GameDataManager.GetPlayerData();
        if (_playerData.everyDayRewardsInfo != null && _playerData.everyDayRewardsInfo[^1] ==
            EveryDayRewardState.WasGotten && _playerData.everyDayRewardsInfo[0] ==
            EveryDayRewardState.WasGotten)
        {
            _freeSkinRewardItem.SetActive(false);
            _playerData.everyDayRewardsInfo = new EveryDayRewardState[_items.Length];
            _playerData.everyDayRewardsInfo[0] = EveryDayRewardState.CanGet;
            for (var i = 1; i < _items.Length; i++) 
            {
                _playerData.everyDayRewardsInfo[i] = EveryDayRewardState.Blocked;
            }
        }
        else if(DateTime.Now.DayOfYear-_playerData.lastCallDate > 1)
        {
            _playerData.everyDayRewardsInfo = new EveryDayRewardState[_items.Length];
            _playerData.everyDayRewardsInfo[0] = EveryDayRewardState.CanGet;
            for (var i = 1; i < _items.Length; i++) 
            {
                _playerData.everyDayRewardsInfo[i] = EveryDayRewardState.Blocked;
            }
        } 
        else if(DateTime.Now.DayOfYear != _playerData.lastCallDate) 
        {
            for (var i = 1; i < _playerData.everyDayRewardsInfo.Length; i++)
            {
                if (_playerData.everyDayRewardsInfo[i] == EveryDayRewardState.CanGet 
                    || _playerData.everyDayRewardsInfo[i] == EveryDayRewardState.WasGotten) continue;
                _playerData.everyDayRewardsInfo[i] = EveryDayRewardState.CanGet;
                break;
            }
        }
        
        
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Init(_playerData.everyDayRewardsInfo[i], i);
        }
        _playerData.lastCallDate = DateTime.Now.DayOfYear;
        GameDataManager.SavePlayerData();
    }

    public void GetReward(int index)
    {
        _rewardEvents[index].Invoke(); 
        _playerData.everyDayRewardsInfo[index] = EveryDayRewardState.WasGotten;
        GameDataManager.SavePlayerData();
    }

    public void AddTurnWheelSpinsReward(int count)
    {
#if UNITY_EDITOR
        var playerData = GameDataManager.GetPlayerData();
        Debug.Log("playerData.freeSpinsCount: " + playerData.freeSpinsCount);
#endif
        _freeSpinsRewardWindow.SetActive(true);
        _freeSpinsRewardWindowText.text = "+" + count;
        GameDataManager.AddFreeSpin(count);
#if UNITY_EDITOR
        playerData = GameDataManager.GetPlayerData();
        Debug.Log("playerData.freeSpinsCount: " + playerData.freeSpinsCount);
#endif
    }

    public void AddMoneyReward(int count)
    {
        _freeMoneyWindow.SetActive(true);
        _freeMoneyWindowText.text = "+" + count;
        CoinManager.Instance.AddCoins(count);
    }

    public void AddSkinReward()
    {
        GameDataManager.AddPurchaseCharacter(_freeSkinIndex);
        _freeSkinRewardWindow.SetActive(true);
    }
}