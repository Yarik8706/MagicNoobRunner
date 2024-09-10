using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int live;
    public int startlive;
    private int plusLive = 0;

    private void OnTriggerEnter(Collider other)
    {
        LiveChanges(plusLive);
    }

    private void LiveChanges(int ChangesLive)
    {
        live = +ChangesLive;
    }
}
