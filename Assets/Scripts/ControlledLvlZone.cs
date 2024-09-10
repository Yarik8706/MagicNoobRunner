using TMPro;
using UnityEngine;

public class ControlledLvlZone : MonoBehaviour
{
    [SerializeField] private Gate _addWeightCount;
    [SerializeField] private Gate _removeWeightCount;
    
    public void Init(int addWeight)
    {
        _addWeightCount.SetValue(addWeight);
        _removeWeightCount.SetValue(-Random.Range(10, 80));
    }
}