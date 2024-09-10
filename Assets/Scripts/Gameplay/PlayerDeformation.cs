using blocks;
using UnityEngine;

public class PlayerDeformation : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _heigth;

    private readonly float _widthMultiplayer = 0.005f;
    private readonly float _heightMultiplayer = 0.02f;

    [SerializeField] private Transform _bonesTransform;
    [SerializeField] private Transform _topSpine;
    [SerializeField] private Transform _bottomSpine;
    [SerializeField] private Transform _colliderTransform;
    [SerializeField] private AudioSource _down;
    [SerializeField] private AudioSource _pump;
    [SerializeField] private AudioSource _bom;

    private int _intermediateWidth;
    private int _intermediateHeight;

    public static PlayerDeformation Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GameEvents.StartGameEvent.AddListener(Init);
    }

    private void Init()
    {
        var _playerData = GameDataManager.GetPlayerData();
        SetWidth(_playerData.activeWidth);
        SetHeigth(_playerData.activeHeight);
    }

    private void Update()
    {
        //_heigth* _heightMultiplayer
        float offsetY = 0.25f;
        _topSpine.position = _bottomSpine.position + new Vector3(0, offsetY + _heigth * _heightMultiplayer, 0);
        _colliderTransform.localScale = new Vector3(1, 1.84f + _heigth * _heightMultiplayer, 1);
    }

    public void AddWidth(int value, bool tip)
    {
        _intermediateWidth = _width;
        _intermediateWidth += value;
        _width = Mathf.Clamp(_intermediateWidth, 0, 500);
        UpdateWidth();
        if (value < 0 & tip == false)
        {
            _down.Play();
        }
        else if (tip == true)
        {
            _bom.Play();
        }
        else if (value > 0)
        {
            _pump.Play();
        }
    }

    public void AddHeigth(int value, bool tip)
    {
        _intermediateHeight = _heigth;
        _intermediateHeight += value;
        _heigth = Mathf.Clamp(_intermediateHeight, 0, 300);
        if (value < 0 & tip == false)
        {
            _down.Play();
        }
        else if (tip)
        {
            _bom.Play();
        }
        else if (value > 0)
        {
            _pump.Play();
        }
    }

    public void SetWidth(int value)
    {
        _width = value;
        UpdateWidth();
    }

    public void SetHeigth(int value)
    {
        _heigth = value;
    }

    public void HitBarrier()
    {
        if (_heigth > 0)
        {
            var newHeight = _heigth - 50;
            _heigth = Mathf.Clamp(newHeight, 0, 300);
        }
        else if (_width > 0)
        {
            var newWidth = _width - 50;
            _width = Mathf.Clamp(newWidth, 0, 300);
            UpdateWidth();
        }
        else
        {
            Die();
        }
    }

    private void UpdateWidth()
    {
        var scale = _bonesTransform.localScale;
        scale.x = 1 + _width * _widthMultiplayer;
        _bonesTransform.localScale = scale;
    }

    private void Die()
    {
        GameEvents.DefeatEvent.Invoke();
        GameManager.Instance.ShowFinishWindowDefeat();
        PlayerBehaviour.Instance.DefeatBehaviour();
    }
}