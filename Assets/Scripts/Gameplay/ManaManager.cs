using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public int mana;
    public int startMana;
    private int plusMana = 0;

    private void OnTriggerEnter(Collider other)
    {
        ManaChanges(plusMana);
    }

    private void ManaChanges(int ChangesMana)
    {
        mana =+ ChangesMana;
    }
}
