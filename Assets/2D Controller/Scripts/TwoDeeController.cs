using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoDeeController
{
	public class TwoDeeController : MonoBehaviour
	{

		private Rigidbody2D rigid2D;

		public float MovementSpeed, JumpStrength, MaxJumpHoldTime;

		private float currentXInput;
		private float currentJumpInput;
		private float jumpTimer=0f;
		private bool isJumping;

		private Vector2 movementVector = new Vector2();

		// Use this for initialization
		void Start()
		{
			rigid2D = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
			currentXInput = RetrieveXInput();
			currentJumpInput = RetrieveJumpInput();

			movementVector.x = currentXInput * MovementSpeed;
			movementVector.y = currentJumpInput * JumpStrength;

			DoMovement();
		}

		private float RetrieveXInput()
		{
			return Input.GetAxis("Horizontal");
		}

		private float RetrieveJumpInput()
		{
			isJumping = Input.GetButton("Jump");

			if (isJumping)
			{
				if (jumpTimer < MaxJumpHoldTime)
				{
					jumpTimer += Time.deltaTime;
					return NormalizedValue(MaxJumpHoldTime,0,jumpTimer);
				}
				else return 0;
			}
			else 
			{
				jumpTimer = 0;
				return 0;
			}
		}

		private void DoMovement()
		{
			rigid2D.AddForce(movementVector);
		}

		private float NormalizedValue(float min, float max, float current)
		{
			return (current - min) / (max - min);
		}
	}
}
