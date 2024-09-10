using UnityEngine;
using Random = UnityEngine.Random;

namespace Unitilies
{
    public class ChooseRandomObject : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomObjects;

        private void Start()
        {
            randomObjects[0].SetActive(false);
            randomObjects[Random.Range(0, randomObjects.Length)].SetActive(true);
        }
    }
}