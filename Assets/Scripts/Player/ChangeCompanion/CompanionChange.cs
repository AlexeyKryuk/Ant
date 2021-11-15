using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompanionChange : MonoBehaviour
{
    [SerializeField] private Jumping _jumping;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _maxAngle;

    private List<Rigidbody> _companions = new List<Rigidbody>();

    public UnityAction<Rigidbody, Companion> CompanionChanged;

    private void Awake()
    {
        foreach (var companion in FindObjectsOfType<Companion>())
        {
            _companions.Add(companion.GetComponent<Rigidbody>());
        }
    }

    private void OnEnable()
    {
        _jumping.JumpEnded += OnJumpEnd;
    }

    private void OnDisable()
    {
        _jumping.JumpEnded -= OnJumpEnd;
    }

    private Rigidbody GetBestTarget()
    {
        Rigidbody target = null;

        foreach (var companion in _companions)
        {
            float distance = Vector3.Distance(transform.position, companion.position);

            if (distance <= _maxDistance)
                target = companion;
        }

        return target;
    }

    private void OnJumpEnd()
    {
        Rigidbody targetRigidbody = GetBestTarget();
        Companion targetCompanion = null;

        if (targetRigidbody != null)
            targetCompanion = targetRigidbody.GetComponent<Companion>();

        CompanionChanged?.Invoke(targetRigidbody, targetCompanion);
    }
}
