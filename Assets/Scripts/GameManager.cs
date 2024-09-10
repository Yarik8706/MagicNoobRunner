using System;
using blocks;
using TMPro;
using UI;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private Transform _startPlayerPosition;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _finishWindowWin;
    [SerializeField] private GameObject _finishWindowDefeat;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private GameplayShop _shop;

    [SerializeField] ManaManager _manaManager;
    [SerializeField] LivesManager _livesManager;

    private GameObject _activeFinishObject;

    public static GameManager Instance;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        Instance = this;
        GameEvents.ResetLevelEvent.AddListener(Init);
    }

    public void StartGame()
    {
        // _livesManager.live = _livesManager.startlive;
        // _manaManager.mana = _manaManager.startMana;
        // LevelsControl.Instance.LoadFirstLevel();
        // GameEvents.ResetLevelEvent.Invoke();
    }

    private void Init()
    {
        _finishWindowDefeat.SetActive(false);
        _finishWindowWin.SetActive(false);
        _startMenu.SetActive(true);
        if (_activeFinishObject != null) Destroy(_activeFinishObject);
        YandexGame.NewLeaderboardScores("Score", YandexGame.savesData.allMoney);
        YandexGame.GetLeaderboard("Score",
            Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
        _activeFinishObject = Instantiate(_finishPrefab, _finishPosition.position, Quaternion.identity);
        PlayerBehaviour.Instance.transform.position = _startPlayerPosition.position;
        _levelText.text = (GameDataManager.GetLevel() + 1).ToString();
        LevelsControl.Instance.RestartLevel();
    }

    public void Play()
    {
        GameEvents.StartGameEvent.Invoke();
        _startMenu.SetActive(false);
        PlayerBehaviour.Instance.Play();
    }

    public void ShowFinishWindowWin()
    {
        _finishWindowWin.SetActive(true);
    }

    public void ShowFinishWindowDefeat()
    {
        _finishWindowDefeat.SetActive(true);
    }

    public void NextLevel()
    {
        LevelsControl.Instance.LoadNextLevel();
        GameEvents.ResetLevelEvent.Invoke();
    }

    public void ReLevel()
    {
        CoinManager.Instance.AddCoins(-CoinManager.Instance.LevelsIncome);
        LevelsControl.Instance.RestartLevel();
        GameEvents.ResetLevelEvent.Invoke();
    }

    public void HomeButtonEvent()
    {
        _startMenu.SetActive(true);
    }
}