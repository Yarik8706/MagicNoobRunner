using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    public class EnemyPartOfBody : MonoBehaviour
    {
        [SerializeField] private int damage;
        
        private Action<Collider, int> _onDamage;

        public void AddDamageListener(Action<Collider, int> onDamage)
        {
            _onDamage = onDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Arrow"))
            {
                _onDamage?.Invoke(other, damage);
            }
        }
    }
}