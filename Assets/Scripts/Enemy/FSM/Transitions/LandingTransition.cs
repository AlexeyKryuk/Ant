using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LandingTransition : Transition
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Player.Jumping.JumpEnded += OnLanding;
    }

    private void OnDisable()
    {
        Player.Jumping.JumpEnded -= OnLanding;
    }

    private void OnLanding()
    {
        if (Player.Target == _rigidbody)
            NeedTransit = true;
    }
}
