using UnityEngine;

public class CoroutineBackgroundControl : MonoBehaviour
{
    public static CoroutineBackgroundControl Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
}