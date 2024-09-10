using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BowMechanic : MonoBehaviour
{
    [SerializeField] private GameObject[] phases;
    [SerializeField] private float phaseDelaySeconds = 0.5f; 
    [SerializeField] private float maxShakeMagnitude = 0.1f;
    [SerializeField] private Transform bow;
    
    private int _currentPhase; 
    private bool _isCharging; 
    private float _lastPhaseTime; 
    private float _currentShakeMagnitude; 
    private Vector3 _shakeVector;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Right mouse button pressed
        {
            _isCharging = true;
            _currentShakeMagnitude = 0.0f;
            _lastPhaseTime = 0;
        }

        if (Input.GetMouseButtonUp(0)) // Right mouse button released
        {
            _isCharging = false;
            ResetPhases();
            _currentShakeMagnitude = 0.0f;
        }

        if (_isCharging && Time.time > _lastPhaseTime + phaseDelaySeconds)
        {
            NextPhase();
            _lastPhaseTime = Time.time;

            if (_currentPhase == phases.Length - 1)
            {
                _currentShakeMagnitude = maxShakeMagnitude; // Set max shake magnitude in the final phase
            }
            else
            {
                _currentShakeMagnitude += (maxShakeMagnitude / (phases.Length - 1)); // Increment the shake magnitude for each phase
            }
        }

        ShakeBow();
    }

    private void NextPhase()
    {
        if (_currentPhase < phases.Length - 1)
        {
            phases[_currentPhase].SetActive(false); // Turn off current phase object
            _currentPhase++;
            phases[_currentPhase].SetActive(true); // Turn on next phase object
        }
    }

    private void ResetPhases()
    {
        foreach (GameObject phase in phases)
        {
            phase.SetActive(false);
        }
        
        _currentPhase = 0; 
        phases[_currentPhase].SetActive(true);
        bow.position -= _shakeVector;
        _shakeVector = Vector3.zero;
    }

    private void ShakeBow()
    {
        bow.position -= _shakeVector;
        _shakeVector.x += Random.Range(-_currentShakeMagnitude, _currentShakeMagnitude);
        _shakeVector.y += Random.Range(-_currentShakeMagnitude, _currentShakeMagnitude);
        bow.position += _shakeVector;
    }
}