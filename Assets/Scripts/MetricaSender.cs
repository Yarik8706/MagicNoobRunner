using System;
using System.Collections.Generic;
using blocks;
using UnityEngine;
using YG;

public class MetricaSender : MonoBehaviour
{
    private void Start()
    {
        GameEvents.StartGameEvent.AddListener(SendLevelStartData);
        GameEvents.WinEvent.AddListener(SendLevelCompleteData);
        GameEvents.DefeatEvent.AddListener(SendLevelFailedData);
    }

    public void SendLevelCompleteData()
    {
        Send("LevelComplete", new Dictionary<string, string>
        {
            {"LevelComplete", GameDataManager.GetLevel().ToString()}
        });
    }

    private void SendLevelFailedData()
    {
        Send("LevelFailed", new Dictionary<string, string>
        {
            {"LevelFailed", GameDataManager.GetLevel().ToString()}
        });
    }

    private void SendLevelStartData()
    {
        Send("LevelStart", new Dictionary<string, string>
        {
            {"LevelStart", GameDataManager.GetLevel().ToString()}
        });
    }


    private void Send(string id, Dictionary<string, string> data)
    {
        YandexMetrica.Send(id, data);
    }
}