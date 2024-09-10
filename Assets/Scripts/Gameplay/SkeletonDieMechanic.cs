using System;
using UnityEngine;

namespace Gameplay
{
    public class SkeletonDieMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Animator _animator;
        [SerializeField] private SkeletonAttackMechanic _skeletonAttackMechanic;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Arrow"))
            {
                if(other.GetComponent<StandardizedProjectile>().authorColliders == _skeletonAttackMechanic._authorColliders) return;
                _animator.enabled = false;
                _skeletonAttackMechanic.enabled = false;
                foreach (var rg in _rigidbodies)
                {
                    rg.isKinematic = false;
                }
            }
        }
    }
}