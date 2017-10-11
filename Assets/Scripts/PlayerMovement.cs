﻿using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D playerRigidbody;

	public float moveAcceleration = 1.0f;
	public float maxHorizontalVelocity = 10.0f;
	public float jumpImpulse = 10.0f;

	public float frictionVelocity = 1.0f;

	private void Update()
	{
		if( Input.GetButtonDown( "Jump" ) )
			playerRigidbody.AddForce( new Vector2( 0.0f, jumpImpulse ), ForceMode2D.Impulse );
	}

	private void FixedUpdate()
	{
		float moveInput = Input.GetAxis( "Horizontal" );

		if( Mathf.Abs( moveInput ) > Mathf.Epsilon )
		{
			if( Mathf.Sign( moveInput ) * playerRigidbody.velocity.x < maxHorizontalVelocity )
			{
				float move = moveInput * moveAcceleration;
				playerRigidbody.AddForce( new Vector2( move, 0.0f ), ForceMode2D.Force );
			}
		}
	}

	private void OnCollisionStay2D( Collision2D collision )
	{
		float moveInput = Input.GetAxis( "Horizontal" );

		if( Mathf.Abs( moveInput ) < Mathf.Epsilon )
		{
			Vector2 vel = playerRigidbody.velocity;
			vel.x *= frictionVelocity * Time.deltaTime;
			playerRigidbody.velocity = vel;
		}
	}
}
