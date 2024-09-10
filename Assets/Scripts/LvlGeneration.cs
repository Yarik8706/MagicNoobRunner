using System;
using blocks;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Collections;

public class LvlGeneration : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _levelTransform;

    public List<LvlZone> _accessibleZones = new();
    private List<LvlZone> _correctZones = new();
    private List<LvlZone> _activeZones = new();
    private int _lvlToZonesByPlayer;
    int _randomNumber;
    bool _inGame = true;
    int _addDistanceForZoneGeneration = 0;

    private void Start()
    {
        // for (int l = 0; l < _accessibleZones.Count + 1; l++)
        // {
        //     if (_accessibleZones[l]._lvlThisZone == _lvlToZonesByPlayer)
        //     {
        //         _correctZones.Add(_accessibleZones[l]);
        //     }
        // }
        // for (int i = 0; i < 7; i++)
        // {
        //     _randomNumber = Random.Range(0, _accessibleZones.Count);
        //     GenerationTheZone(_randomNumber);
        //     _addDistanceForZoneGeneration = +10;
        //     _activeZones.Add(_correctZones[_randomNumber]);
        // }
        // StartGenerationTheLvl();

    }
    private IEnumerator StartGenerationTheLvl()
    {
        for (; ; )
        {
            if (_inGame)
            {
                _randomNumber = Random.Range(0, _accessibleZones.Count);
                GenerationTheZone(_randomNumber);
                _addDistanceForZoneGeneration = +10;
                _activeZones.Add(_correctZones[_randomNumber]);
                Destroy(_accessibleZones[0]);

                yield return new WaitForSeconds(1);
            }
        }
    }
    private void GenerationTheZone(int _randomNumber)
    {
        Instantiate(_correctZones[_randomNumber], new Vector3(0, 0, _spawnPoint.position.z + _addDistanceForZoneGeneration), Quaternion.identity);
    }


    public void DestroyAllZones()
    {
        foreach (var t in _activeZones)
        {
            Destroy(t);
        }
        _activeZones.Clear();
    }
}
