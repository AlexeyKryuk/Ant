using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class Jumping : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _takeoff;
    [SerializeField] private float _jumpForce;

    private Player _player;

    public bool IsInFlight { get; private set; }

    public UnityAction Jumped;
    public UnityAction JumpEnded;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        JumpSpeedRegulation();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void JumpSpeedRegulation()
    {
        if (_player.Target.velocity.y < 0)
            _player.Target.velocity += Vector3.up * Physics.gravity.y * _fallSpeed * Time.deltaTime;
        else if (_player.Target.velocity.y > 0)
            _player.Target.velocity += Vector3.up * Physics.gravity.y * _takeoff * Time.deltaTime;
    }

    private void Jump()
    {
        Jumped?.Invoke();
        _player.Target.velocity = (Vector3.up + Vector3.forward / 3) * _jumpForce;
        StartCoroutine(TurnOffGravity(_duration));
    }

    private IEnumerator TurnOffGravity(float duration)
    {
        float initialFallSpeed = _fallSpeed;
        _fallSpeed = 0f;

        IsInFlight = true;
        yield return new WaitForSeconds(duration);
        IsInFlight = false;

        JumpEnded?.Invoke();
        _fallSpeed = initialFallSpeed;
    }
}