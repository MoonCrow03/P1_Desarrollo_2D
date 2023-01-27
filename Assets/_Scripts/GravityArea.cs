using System;
using UnityEngine;

public class GravityArea : MonoBehaviour
{
    private bool _playerInRange;
    private void OnEnable()
    {
        Jumping.OnChangeGravity += ChangeGravity;
    }

    private void OnDisable()
    {
        Jumping.OnChangeGravity -= ChangeGravity;
    }

    private void ChangeGravity()
    {
        if (_playerInRange)
            Physics2D.gravity = new Vector2(0f, 9.81f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in Range");
            _playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is NOT in Range");
            _playerInRange = false;
        }
    }
}
