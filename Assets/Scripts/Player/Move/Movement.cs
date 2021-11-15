using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Movement: MonoBehaviour
{
    [SerializeField] private Joystick _input;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Player _player;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_input.Horizontal * _rotateSpeed, 0f, 1f);
        Move(direction, _player.Target.transform);
    }

    public void Move(Vector3 direction, Transform targetTransform)
    {
        targetTransform.Translate(direction * _moveSpeed * Time.deltaTime);
        Rotate(direction, targetTransform);
    }

    private void Rotate(Vector3 direction, Transform targetTransform)
    {
        Vector3 lookDirection = direction + targetTransform.position;
        targetTransform.LookAt(new Vector3(lookDirection.x, targetTransform.position.y, lookDirection.z));
    }
}