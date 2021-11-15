using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Jumping _jumping;
    [SerializeField] private Rigidbody _startTarget;

    private Rigidbody _target;
    private Rigidbody _ownRigidbody;

    public Movement Movement => _movement;
    public Jumping Jumping => _jumping;
    public Rigidbody Target => _target;

    public UnityAction Died;

    private void Awake()
    {
        _ownRigidbody = GetComponent<Rigidbody>();
        SetTarget(_startTarget);
    }

    private void OnEnable()
    {
        _jumping.Jumped += OnJump;
    }

    private void OnDisable()
    {
        _jumping.Jumped -= OnJump;
    }

    private void OnJump()
    {
        SetTarget(_ownRigidbody);
    }

    public void SetTarget(Rigidbody target)
    {
        if (_ownRigidbody == target || target == null)
        {
            _target = _ownRigidbody;
            transform.parent = null;
        }
        else
        {
            _target = target;
            transform.SetParent(target.transform);
        }
    }
}
