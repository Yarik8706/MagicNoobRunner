using System;
using UnityEngine;

namespace Gameplay
{
    public class SkeletonDieMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Animator _animator;
        [SerializeField] private SkeletonAttackMechanic _skeletonAttackMechanic;
        [SerializeField] private EnemyPartOfBody[] _enemyPartOfBodies;
        [SerializeField] private int _lives = 3;

        private void Start()
        {
            foreach (var partOfBody in _enemyPartOfBodies)
            {
                partOfBody.AddDamageListener(OnDamageEvent);
            }
        }

        private void OnDamageEvent(Collider other, int damage)
        {
            if (other.attachedRigidbody.GetComponent<StandardizedProjectile>().authorColliders ==
                _skeletonAttackMechanic._authorColliders) return;
            _lives -= damage;
            if (_lives <= 0)
            {
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