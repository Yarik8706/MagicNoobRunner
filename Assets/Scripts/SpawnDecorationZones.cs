using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace blocks
{
    public class SpawnDecorationZones : MonoBehaviour
    {
        [SerializeField] private Transform _decorationsParent;
        [SerializeField] private GameObject[] _decorationZones;
        [SerializeField] private GameObject[] _decorationZoneEnds;
        [SerializeField] private float _decorationLength = 15;

        private void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                SpawnDecorationZone(i, 1);
            }
            for (int i = 0; i < 6; i++)
            {
                SpawnDecorationZone(i, -1);
            }

            var endZone = Instantiate(_decorationZoneEnds[Random.Range(0, _decorationZoneEnds.Length)],
                _decorationsParent);
            endZone.transform.position = Vector3.forward * 90;
            if (Random.Range(0, 2) != 1) return;
            var scale = endZone.transform.localScale;
            scale.x = -1;
            endZone.transform.localScale = scale;
        }

        private void SpawnDecorationZone(int decorationCount, int scaleX)
        {
            var decorationZone = Instantiate(_decorationZones[Random.Range(0, _decorationZones.Length)],
                _decorationsParent);
            decorationZone.transform.position = _decorationLength * decorationCount * Vector3.forward;
            var scale = decorationZone.transform.localScale;
            scale.x = scaleX;
            decorationZone.transform.localScale = scale;
        }
    }
}