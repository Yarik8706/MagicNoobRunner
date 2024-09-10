using DG.Tweening;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _run;

    public static PlayerBehaviour Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GameEvents.ResetLevelEvent.AddListener(Init);
    }

    private void Init()
    {
        return;
        transform.rotation = Quaternion.identity;
        _animator.SetBool("Dance", false);
        _animator.Play("Idle");
        _run.Stop();
        _win.Stop();
        _playerMove.enabled = false;
    }

    public void Play()
    {
       
    }

    public void StartPreFinishBehaviour()
    {
        _playerMove.enabled = false;
    }

    public void WinBehaviour()
    {
        transform.DORotate(Vector3.up * 180, 0.7f);
        _animator.SetBool("Dance", true);
        _run.Stop();
        _win.Play();
    }

    public void DefeatBehaviour()
    {
        return;
        _animator.SetTrigger("Defeat");
        _run.Stop();
    }
}