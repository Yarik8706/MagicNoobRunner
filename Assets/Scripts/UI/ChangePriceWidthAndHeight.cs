using blocks;
using TMPro;
using UnityEngine;

public class ChangePriceWidthAndHeight : MonoBehaviour
{
    [SerializeField] private TMP_Text _heightPrice;
    [SerializeField] private TMP_Text _widthPrice;

    public static ChangePriceWidthAndHeight Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void UpdatePrice()
    {
        var playerData = GameDataManager.GetPlayerData();
        _heightPrice.text = playerData.heightPrice.ToString();
        _widthPrice.text = playerData.widthPrice.ToString();
    }
}