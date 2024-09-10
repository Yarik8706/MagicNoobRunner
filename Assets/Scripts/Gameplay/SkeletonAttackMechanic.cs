using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class SkeletonAttackMechanic : StandardizedBow
    {
        [SerializeField] private GameObject[] phases;
        [SerializeField] private float phaseDelaySeconds = 0.5f;
        [SerializeField] private Transform skeleton;
        [SerializeField] private float _startAttackDistance = 8f;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _rotationSpeed = 1f;
 
        private int _currentPhase;
        private bool _isCharging;
        private bool _isAttacking;
        private float _lastPhaseTime;
        private bool _isPreparing;

        protected override void Start()
        {
            base.Start();
            stringMoveTime = 0.1f;
        }

        protected override bool CanIShoot()
        {
            return _isAttacking && !_isCharging;
        }

        protected override bool CanILoadBow()
        {
            return _isCharging || _isPreparing;
        }

        protected override void Update()
        {
            if (!_isAttacking && !_isPreparing && _startAttackDistance >= 
                Vector2.Distance(new Vector2(transform.position.x, transform.position.z), 
                    new Vector2(PlayerBehaviour.Instance.transform.position.x, 
                        PlayerBehaviour.Instance.transform.position.z))) // Right mouse button pressed
            {
                _isPreparing = true;
                _animator.SetBool("IsAttack", true);
                StartCoroutine(WaitPreparingForAttackCoroutine());
            }

            if (_isAttacking || _isPreparing)
            {
                Vector3 currentRotation = skeleton.rotation.eulerAngles;
                if(PlayerBehaviour.Instance == null) return;
                skeleton.LookAt(PlayerBehaviour.Instance.transform);
                Vector3 rotationToPlayer = skeleton.rotation.eulerAngles;
                rotationToPlayer = new Vector3(
                    currentRotation.x, 
                    Mathf.Lerp(currentRotation.y, 
                        rotationToPlayer.y + 90, 
                        _rotationSpeed*Time.deltaTime),
                    currentRotation.z);

                var rotation = skeleton.rotation;
                rotation.eulerAngles = rotationToPlayer;
                skeleton.rotation = rotation;
            }

            if (_currentPhase == phases.Length - 1 && Time.time > _lastPhaseTime + phaseDelaySeconds)
            {
                _isCharging = false;
                ResetPhases();
            }

            if (_isCharging && Time.time > _lastPhaseTime + phaseDelaySeconds)
            {
                NextPhase();
                _lastPhaseTime = Time.time;
            }
            base.Update();
        }

        private IEnumerator WaitPreparingForAttackCoroutine()
        {
            yield return new WaitForSeconds(1f);
            _isAttacking = true;
            _isCharging = true;
            _isPreparing = false;
            _lastPhaseTime = 0;
        }

        protected override void ShootProjectile(float currentPower)
        {
            _isAttacking = _startAttackDistance >= Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z), 
                new Vector2(PlayerBehaviour.Instance.transform.position.x, 
                    PlayerBehaviour.Instance.transform.position.z));
            if (_isAttacking)
            {
                _isCharging = true;
                _isPreparing = false;
                _lastPhaseTime = 1f;
            }
            _animator.SetBool("IsAttack", _isAttacking);
            base.ShootProjectile(currentPower);
        }

        private void NextPhase()
        {
            if (_currentPhase < phases.Length - 1)
            {
                phases[_currentPhase].SetActive(false); // Turn off current phase object
                _currentPhase++;
                phases[_currentPhase].SetActive(true); // Turn on next phase object
            }
        }

        private void ResetPhases()
        {
            foreach (GameObject phase in phases)
            {
                phase.SetActive(false);
            }

            _currentPhase = 0;
            phases[_currentPhase].SetActive(true);
        }
    }
}