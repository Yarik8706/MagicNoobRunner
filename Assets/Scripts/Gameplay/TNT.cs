using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerDeformation.Instance.AddWidth(-30, true);
        PlayerDeformation.Instance.AddHeigth(-30, true);
        Destroy(gameObject);
    }
} 
