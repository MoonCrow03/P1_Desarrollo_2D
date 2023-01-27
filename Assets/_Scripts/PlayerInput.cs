using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }

    public static Action OnJump;
    
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping");
            OnJump?.Invoke();
        }
    }
}
