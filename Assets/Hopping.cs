﻿using UnityEngine;

// This behavior makes a body randomly hop around
public class Hopping : MonoBehaviour {

	// Config constants
	public float Speed = 2.0f;
	public float JumpStrength = 10.0f;
	public float JumpCooldown = 0.5f;

	// Components
	private Rigidbody _body;
	private Directional _direction;
	private Grounded _ground;

	// State
	private float _lastJump;

	private void Start () {
		_body = GetComponent<Rigidbody>();
		_direction = GetComponent<Directional>();
		_ground = GetComponent<Grounded>();
	}

	private void FixedUpdate()
	{
		if (Random.Range(0, 10) > 1) return;
		if (!_ground.IsGrounded() || !_ground.IsUpright()) return;
		if (_direction.CheckDirection()) {
			Jump();	
		} else {
			Debug.Log("Change direction");
			_direction.RandomDirection();
		}
	}

	private void Jump() {
		//body.velocity = Vector3.up * 2;
		if (Time.time < _lastJump + JumpCooldown) return;
		_lastJump = Time.time;
		var jump = Vector3.up * JumpStrength + _direction.GetDirection() * Speed;
		_body.AddForce(jump.x, jump.y, jump.z, ForceMode.Impulse);
	}
}