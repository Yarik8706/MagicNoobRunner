using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDieMechanic : MonoBehaviour
{
    [SerializeField] private Collider _playerCollider;
    
    private int _lives = 3;
    
    public static PlayerDieMechanic Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow") && other.transform.parent.parent.TryGetComponent(out StandardizedProjectile projectile) 
                                      && projectile.authorColliders[0] != _playerCollider)
        {
            Die();
        }   
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}