using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody))]
public class Landing : MonoBehaviour
{
    [SerializeField] private CompanionChange _companionChange;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _attractionSpeed;

    private Player _player;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _companionChange.CompanionChanged += OnCompanionChanged;
    }

    private void OnDisable()
    {
        _companionChange.CompanionChanged -= OnCompanionChanged;
    }

    private void OnCompanionChanged(Rigidbody target, Companion companion)
    {
        _movement.enabled = false;

        if (target != null)
        {
            StartCoroutine(MoveToTarget(target, companion));
        }
        else
        {
            Debug.Log("GameOver");
        }
    }

    private IEnumerator MoveToTarget(Rigidbody target, Companion companion)
    {
        while ((transform.position.y - transform.localScale.y / 2 - companion.Anchor.position.y) > 0.1f)
        {
            Debug.Log("Player pos " + companion.Anchor.localPosition.y + transform.localScale.y / 2);
            Debug.Log("Anchor pos " + companion.Anchor.position.y);
            Vector3 targetPosition = new Vector3(companion.Anchor.position.x, companion.Anchor.localPosition.y + transform.localScale.y / 2, companion.Anchor.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, _attractionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.identity;

            Debug.Log("Landing in progress");
            yield return new WaitForFixedUpdate();
        }

        _rigidbody.velocity = Vector3.zero;
        _player.SetTarget(target);
        _movement.enabled = true;
    }
}
