using blocks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EveryDayRewardItem : MonoBehaviour
{
    [SerializeField] private Button _getRewardedButton;
    [SerializeField] private GameObject _selectedImage;

    public void Init(EveryDayRewardState state, int index)
    {
        _selectedImage.SetActive(state == EveryDayRewardState.WasGotten);
        _getRewardedButton.gameObject.SetActive(state == EveryDayRewardState.CanGet);
        _getRewardedButton.onClick.AddListener(() => { 
            _selectedImage.SetActive(true);
            EveryDayRewardUI.Instance.GetReward(index);
            _getRewardedButton.gameObject.SetActive(false);
            });
    }
}