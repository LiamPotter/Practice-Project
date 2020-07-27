using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollPlayer : MonoBehaviour {

	private Rigidbody2D playerRigid;

	public float JumpStrength, MaxJumpHoldTime;

	private float currentJumpInput;
	private float jumpTimer = 0f;
	private bool isJumping;
	private bool jumpInput;

	private Vector2 jumpVector = new Vector2();

	public float GroundedHeight;

	private Ray2D groundRay;
	private RaycastHit2D groundHit;
	private bool isGrounded;

	// Use this for initialization
	void Start ()
	{
		playerRigid = GetComponent<Rigidbody2D>();
		groundRay = new Ray2D();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded = CheckGrounded();
		jumpVector.y = RetrieveJumpInput()*JumpStrength;
		PerformJump();
	}

	private void PerformJump()
	{
		playerRigid.AddForce(jumpVector, ForceMode2D.Impulse);
	}

	private bool CheckGrounded()
	{
		groundRay.origin = transform.position;
		groundRay.direction = Vector3.down;
		groundHit = Physics2D.Raycast(groundRay.origin, groundRay.direction, GroundedHeight);
		if (groundHit.collider != null)
		{
			return true;
		}
		else return false;
	}

	private float RetrieveJumpInput()
	{
		jumpInput = Input.GetButton("Jump");
		if (jumpInput && isGrounded)
			isJumping = true;
		else if (jumpInput && !isGrounded)
		{
			if (jumpTimer >= MaxJumpHoldTime)
			{
				isJumping = false;
				return 0;
			}
		}
		else if (!jumpInput)
			isJumping = false;

		if (isJumping)
		{
			if (jumpTimer < MaxJumpHoldTime)
			{
				jumpTimer += Time.deltaTime;
				return NormalizedValue(MaxJumpHoldTime, 0, jumpTimer);
			}
			else
			{
				isJumping = false;
				return 0;
			}
		}
		else
		{
			jumpTimer = 0;
			return 0;
		}
	}

	private float NormalizedValue(float min, float max, float current)
	{
		return (current - min) / (max - min);
	}
}
