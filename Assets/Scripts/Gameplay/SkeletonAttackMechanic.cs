using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class SkeletonAttackMechanic : StandardizedBow
    {
        [SerializeField] private float _armsRotationSpeed = 1f;
        [SerializeField] private float phaseDelaySeconds = 0.5f;
        [SerializeField] private float _startAttackDistance = 8f;
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private float _waitForAttackTime = 2f;
        
        [SerializeField] private GameObject[] phases;
        [SerializeField] private Transform skeleton;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _lArm;
        [SerializeField] private Transform _rArm;
        [SerializeField] private Transform _rArmEnd;
 
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
                Vector3 direction = PlayerBehaviour.Instance.transform.position - skeleton.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.right);
                Quaternion targetRotationY = Quaternion.Euler(0f, targetRotation.eulerAngles.y+90, 0f);
                skeleton.rotation = Quaternion.RotateTowards(skeleton.transform.rotation, targetRotationY, _rotationSpeed * Time.deltaTime);
                
                // RotateArmsToPlayer();
                // RotateBowToPlayer();
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
            yield return new WaitForSeconds(_waitForAttackTime);
            _isAttacking = true;
            _isCharging = true;
            _isPreparing = false;
            _lastPhaseTime = 0;
            // FirstRotateArmsForAttack();
        }

        private void FirstRotateArmsForAttack()
        {
            _lArm.rotation = Quaternion.Euler(-69f, -55f, 145f);
            _rArm.rotation = Quaternion.Euler(71f, 56f, 145f);
        }

        // private void RotateArmsToPlayer()
        // {
        //     Vector3 direction = PlayerBehaviour.Instance.transform.position - _lArm.position;
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     Quaternion targetRotationZ = Quaternion.Euler(0f, 0f, targetRotation.eulerAngles.z);
        //     _lArm.rotation = Quaternion.RotateTowards(_lArm.rotation, 
        //         targetRotationZ, _armsRotationSpeed * Time.deltaTime);
        //     _rArm.rotation = Quaternion.RotateTowards(_lArm.rotation, 
        //         targetRotationZ, _armsRotationSpeed * Time.deltaTime);
        // }
        //
        // private void RotateBowToPlayer()
        // {
        //     Vector3 direction = PlayerBehaviour.Instance.transform.position - _rArmEnd.position;
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     Quaternion targetRotationZ = Quaternion.Euler(targetRotation.eulerAngles.x, 0f, 0);
        //     _rArmEnd.rotation = Quaternion.RotateTowards(_rArmEnd.rotation, 
        //         targetRotationZ, _armsRotationSpeed * Time.deltaTime);
        // }

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