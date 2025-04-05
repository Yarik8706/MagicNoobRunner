using System;
using UnityEngine;

namespace Gameplay
{
    public class ZombieDieMechanic : MonoBehaviour
    {
        [SerializeField] private EnemyPartOfBody[] _enemyPartOfBodies;
        [SerializeField] private Animator _animator;
        [SerializeField] private ZombieAttackMechanic _zombieAttackMechanic;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private int _lives = 3;
        [SerializeField] private Collider _zombieCollider;

        private void Start()
        {
            foreach (var partOfBody in _enemyPartOfBodies)
            {
                partOfBody.AddDamageListener(OnDamageEvent);
            }
        }

        private void OnDamageEvent(Collider other, int damage)
        {
            _lives -= damage;
            if (_lives <= 0)
            {
                _zombieCollider.enabled = false;
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