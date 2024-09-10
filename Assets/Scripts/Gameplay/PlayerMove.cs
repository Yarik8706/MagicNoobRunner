
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _run;
    
    private float _oldMousePositionX;
    private float _eulerY;

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     _oldMousePositionX = Input.mousePosition.x;
        //     _animator.SetBool("Run", true);
        //     _run.Play();
        // }
        // else if (Input.GetMouseButton(0))
        // {
        //     Vector3 newPosition = transform.position + transform.forward * Time.deltaTime * _speed;
        //     newPosition.x = Mathf.Clamp(newPosition.x, -2.9f, 2.9f);
        //     transform.position = newPosition;
        //     float deltaX = Input.mousePosition.x - _oldMousePositionX;
        //     _oldMousePositionX = Input.mousePosition.x;
        //     _eulerY += deltaX;
        //     _eulerY = Mathf.Clamp(_eulerY, -70, 70);
        //     transform.eulerAngles = new Vector3(0, _eulerY * 0.7f, 0);
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     _animator.SetBool("Run", false);
        //     _run.Stop();
        // }
        // transform.position += transform.forward * Time.deltaTime * _speed.;
    }

    private void OnDisable()
    {
        _animator.SetBool("Run", false);
    }
}
