using TMPro;
using UI;
using UnityEngine;

public class ShowLevelsIncome : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;

    private void Start()
    {
        GameEvents.UpdateMoneyCount.AddListener(ShowIncome);
        GameEvents.WinEvent.AddListener(ShowIncome);
        GameEvents.DefeatEvent.AddListener(ShowIncome);  
    }

    private void ShowIncome()
    {
        foreach (var text in _texts)
        {
            text.text = CoinManager.Instance.LevelsIncome.ToString();
        }
    }
}