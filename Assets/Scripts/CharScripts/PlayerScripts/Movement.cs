﻿using System;
using UnityEngine;

public class Movement : MonoBehaviour {


    private Animator _anim;
    private const float MoveForce = 270f;
    private float _speed = 1f;
    private const float JumpSpeed = 700f;
    private const float OriginalSpeed = 0.7f;
    private const float MaxRunSpeed = 2f;

    private bool _onGround = true;
    private bool _jump = true;
    private bool _goingRight = true;
    

    private Rigidbody2D _playerRb;
    public Transform GroundCheck;

    private const int _enviMask = 10;
    private const int _groundMask = 15;
    private LayerMask _detectEnvi;
    private LayerMask _detectGround;
    private LayerMask _jumpLayerMask;
    
    
	void Awake ()
	{
	    _anim = gameObject.GetComponent<Animator>();
	    _playerRb = gameObject.GetComponent<Rigidbody2D>();
	    _detectEnvi = 1 << _enviMask;
	    _detectGround = 1 << _groundMask;

	    _jumpLayerMask = _detectEnvi | _detectGround;
	}
	
	/* 
     * For every frame, checks if player is on ground and if the running button was held or not
     * If player is on ground, set jump to true for frame
     */
	void Update ()
	{

	    _onGround = Physics2D.Linecast(gameObject.transform.position, GroundCheck.position, _jumpLayerMask);
        
	    if (Input.GetKeyDown(KeyCode.Space) && _onGround)
	    {
	        _jump = true;
	    }

	    if (Input.GetKeyDown(KeyCode.LeftShift))
	    {
	        _speed = MaxRunSpeed;
	    }
	    else if (Input.GetKeyUp(KeyCode.LeftShift))
	    {
	        _speed = OriginalSpeed;
	    }
	}

    /*
     * Moves player according to button pressed (Input Mng), and change sprite accordingly
     * Also jumps if jump flag is true for last frame
     */
    void FixedUpdate()
    {
        float axisPress = Input.GetAxisRaw("Horizontal");

        _anim.SetFloat("Speed", Math.Abs(axisPress));

        if (axisPress*_playerRb.velocity.x < _speed)
        {
            _playerRb.AddForce(Vector2.right * axisPress * MoveForce);
        }

        if (Math.Abs(_playerRb.velocity.x) > _speed)
        {
            _playerRb.velocity = new Vector2(Math.Sign(_playerRb.velocity.x) * _speed, _playerRb.velocity.y);
        }

        if (axisPress < 0 && _goingRight)
        {            
            Flip();
        }
        else if (axisPress > 0 && !_goingRight)
        {
            Flip();
        }

        if (_jump)
        {
            _playerRb.AddForce(new Vector2(0, JumpSpeed));
            _jump = false;
        }
        
    }

    /*
     * Flips sprite
     */
    private void Flip()
    {
            _goingRight = !_goingRight;
            Vector3 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
    }
    
}