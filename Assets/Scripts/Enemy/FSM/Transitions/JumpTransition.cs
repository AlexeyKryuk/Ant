using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTransition : Transition
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Player.Jumping.Jumped += OnJump;
    }

    private void OnDisable()
    {
        Player.Jumping.Jumped -= OnJump;
    }

    private void OnJump()
    {
        NeedTransit = true;
    }
}
