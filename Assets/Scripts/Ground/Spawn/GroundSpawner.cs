using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distanceToSpawn;

    private Transform _current;
    private Transform _next;

    private void Awake()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        _current = children[1];
        _next = children[2];
        _next.gameObject.SetActive(false);
    }

    private void Update()
    {
        TryToSpawn();
    }

    private void AddGround()
    {
        _next.transform.position = new Vector3(_current.transform.position.x, _current.transform.position.y, _current.transform.position.z + _current.localScale.z);
        _next.gameObject.SetActive(true);
    }

    private void RemoveGround()
    {
        _current.transform.position = new Vector3(_next.transform.position.x, _next.transform.position.y, _next.transform.position.z + _next.localScale.z);
        _current.gameObject.SetActive(false);

        Transform temp = _current;
        _current = _next;
        _next = temp;
    }

    private void TryToSpawn()
    {
        float distance = (_player.position - _current.transform.position).z;

        if (distance > _current.localScale.z / 2 - _distanceToSpawn)
            AddGround();

        if (distance > _current.localScale.z / 2 + _distanceToSpawn)
            RemoveGround();
    }
}
