using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public Player Player { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Player target)
    {
        Player = target;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }

    protected Collider GetCurrentPlatform()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hit;

        LayerMask layer = LayerMask.GetMask("Platform");
        float maxDistance = 10f;

        if (Physics.Raycast(ray, out hit, maxDistance, layer))
        {
            return hit.collider;
        }
        else
            return null;
    }
}
