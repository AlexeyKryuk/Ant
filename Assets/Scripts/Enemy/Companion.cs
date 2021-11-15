using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private Player _target;
    
    private Transform _anchor;

    public Player Target => _target;
    public Transform Anchor => _anchor;

    private void Awake()
    {
        _anchor = GetComponentInChildren<Transform>();
    }
}
