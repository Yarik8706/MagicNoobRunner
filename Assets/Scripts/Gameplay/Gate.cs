using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private DeformationType _deformationType;
    [SerializeField] private GateApperaence _gateApperaence;

    public void SetValue(int value)
    {
        _value = value;
        _gateApperaence.UpdateVisual(_deformationType, _value);
    }

    private void OnValidate()
    {
        _gateApperaence.UpdateVisual(_deformationType, _value);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDeformation pLayerDeformation = other.attachedRigidbody.GetComponent<PlayerDeformation>();
        if (pLayerDeformation)
        {
            if (_deformationType == DeformationType.Width)
            {
                pLayerDeformation.AddWidth(_value, false);
            }
            else
            {
                pLayerDeformation.AddHeigth(_value, false);
            }
            Destroy(gameObject);
        }
    }
}