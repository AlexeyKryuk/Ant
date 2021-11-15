using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Vector3 target = new Vector3(_target.position.x + _offset.x, transform.position.y, _target.position.z + _offset.z);
        transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
    }
}
