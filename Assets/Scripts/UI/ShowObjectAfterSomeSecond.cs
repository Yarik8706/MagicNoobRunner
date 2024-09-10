using System.Collections;
using UnityEngine;

public class ShowObjectAfterSomeSecond : MonoBehaviour
{
    [SerializeField] private float _afterSomeSecond;
    [SerializeField] private GameObject _object;

    private void Start()
    {
        StartCoroutine(ShowObjectCoroutine());
    }

    private IEnumerator ShowObjectCoroutine()
    {
        _object.SetActive(false);
        yield return new WaitForSeconds(_afterSomeSecond);
        _object.SetActive(true);
    }

}