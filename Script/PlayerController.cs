using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _gravityDrag = .8f;
    Rigidbody2D _rb = default;
    bool _isGrounded = true;
    Vector3 _initialPosition = default;
    SpriteRenderer _sprite;
    float h;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;
        
    }
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = _rb.velocity;

       
        if (h != 0)
        {
            velocity.x = h * _moveSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpSpeed;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            velocity.y *= _gravityDrag;
        }

        _rb.velocity = velocity;
    }
  
    public void JumpSpeeed(float jump)
    {
        _jumpSpeed += jump;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }

}

