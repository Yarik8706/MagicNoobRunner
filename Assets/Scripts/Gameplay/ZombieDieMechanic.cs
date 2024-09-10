using System;
using UnityEngine;

namespace Gameplay
{
    public class ZombieDieMechanic : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ZombieAttackMechanic _zombieAttackMechanic;
        [SerializeField] private Rigidbody[] _rigidbodies;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Arrow"))
            {
                _animator.enabled = false;
                _zombieAttackMechanic.enabled = false;
                foreach (var rg in _rigidbodies)
                {
                    rg.isKinematic = false;
                }
            }
        }
    }
}