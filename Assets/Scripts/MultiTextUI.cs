using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultiText
{
    public string ruText;
    public string enText;

    public MultiText(string ruText, string enText)
    {
        this.ruText = ruText;
        this.enText = enText;
    }

    public string GetText()
    {
        return MultiTextUI.lang == "ru" ? ruText : enText;
    }
}

public class MultiTextUI : MonoBehaviour
{
    [SerializeField] private string ruText;
    [SerializeField] private string enText;

    private TMP_Text _text;

    public static string lang;

    public static UnityEvent ChangeLanguageEvent = new UnityEvent();

    private void Awake()
    {
        ChangeLanguageEvent.AddListener(SetText);
    }

    private void OnEnable()
    {
        SetText();
    }

    private void SetText()
    {
        if (TryGetComponent(out _text))
        {
            _text.text = lang == "ru" ? ruText : enText;
        }
        else
        {
            GetComponent<Text>().text = lang == "ru" ? ruText : enText;
        }
    }
}