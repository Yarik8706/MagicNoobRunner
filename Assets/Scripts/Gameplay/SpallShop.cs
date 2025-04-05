using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpallShop : MonoBehaviour
{
    [SerializeField] GameObject _canvasSpalls;
    [SerializeField] TextMeshProUGUI _nameOfSpall1;
    [SerializeField] TextMeshProUGUI _nameOfSpall2;
    [SerializeField] TextMeshProUGUI _priceOfSpall1;
    [SerializeField] TextMeshProUGUI _priceOfSpall2;
    int _levlOfSpall1;
    int _levlOfSpall2;
    int _intermedialPrice;
    int[] _upgradeSpall1 = new int[] { 30, 10 };
    int[] _upgradeSpall2 = new int[] { 60, 15 };
    int[] _priceOfSpall = new int[] { 5, 5 };
    int[] _priceIncrease = new int[] { 2, 2 };
    string[] _nameSpall = new string[] { "Spall1", "Spall2" };

    private void Start()
    {
        _levlOfSpall1 = 0;
        _levlOfSpall2 = 0;
        _nameOfSpall1.text = _nameSpall[0];
        _nameOfSpall2.text = _nameSpall[1];
        _priceOfSpall1.text = Convert.ToString(_priceOfSpall[0]);
        _priceOfSpall2.text = Convert.ToString(_priceOfSpall[1]);
        _canvasSpalls.SetActive(false);
    }

    public void PriceSpall1()
    {
        if (_upgradeSpall1.Length > _levlOfSpall1)
        {
            //списать деньги  
            _intermedialPrice = Convert.ToInt32(_priceOfSpall1.text);
            _intermedialPrice += _priceIncrease[0];
            _priceOfSpall1.text = Convert.ToString(_intermedialPrice);
            //какая-то переменная = _upgradeSpall1[_levlOfSpall1]    кароче просто что-то улучшается, и в массиве должны быть велечины, на которые время отката и тд, улудшается
            _levlOfSpall1++;
        }
        if (_upgradeSpall1.Length == _levlOfSpall1)
        {
            _nameOfSpall1.text = "Max Level";
            _priceOfSpall1.text = "Max Level";
        }
    }
    
    public void PriceSpall2()
    {
        if (_upgradeSpall2.Length > _levlOfSpall2)
        {
            //списать деньги  
            _intermedialPrice = Convert.ToInt32(_priceOfSpall2.text);
            _intermedialPrice += _priceIncrease[1];
            _priceOfSpall2.text = Convert.ToString(_intermedialPrice);
            //какая-то переменная = _upgradeSpall2[_levlOfSpall2]    кароче просто что-то улучшается, и в массиве должны быть велечины, на которые время отката и тд, улудшается
            _levlOfSpall2++;
        }
        if (_upgradeSpall2.Length == _levlOfSpall2)
        {
            _nameOfSpall2.text = "Max Level";
            _priceOfSpall2.text = "Max Level";
        }
    }
    
    public void CloseButtonPressed()
    {
        _canvasSpalls.SetActive(false);
    }
    
    public void UpgradeToSpalls()
    {
        _canvasSpalls.SetActive(true);
    }
}
