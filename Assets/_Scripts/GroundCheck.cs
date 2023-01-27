using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private float radious = 0.15f;
    public bool IsGrounded => _isGrounded;
    /**
     *  Es una mezcla de variable y método
     *  public bool IsGrounded{
     *      get{ return _isGrounded;}
     *  }
     */
    private bool _isGrounded;

    [SerializeField] private LayerMask WhatIsGround;
    
    // Update va más con el renderizar y el player input (todos los scripts). Lo hace siempre y muy rápido.
    // FixedUpdate va un ritmo marcado y antes de las operaciones de la física.
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, radious, WhatIsGround);
        _isGrounded = colliders.Length > 0;
    }
}
