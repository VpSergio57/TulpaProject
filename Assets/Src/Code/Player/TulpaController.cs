using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulpaController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement.
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping.
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character.
	[SerializeField] private GameObject tulpaFire;
	private GameObject atackPoint;
	private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	private Transform m_ChestCheck;
	private Rigidbody2D body;
	private Animator myAnimation;
	private Vector3 velocity = Vector3.zero;
	public const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private float timeToShootAgain=0;
	

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		myAnimation = GetComponent<Animator>();
		m_GroundCheck = transform.Find("GroundCheck");
		m_ChestCheck = transform.Find("ChestCheck");
		Transform atackChildTransform = transform.Find("AtackPoint");
		atackPoint = (atackChildTransform != null ? atackChildTransform.gameObject : null);
		
	}


	private void FixedUpdate()
	{
		timeToShootAgain += Time.deltaTime;
		m_Grounded = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}
	}
	public void Punching(){
		StartCoroutine(WhaitGOTime(atackPoint));
		if(Random.Range(0,4) > 1)
        {
			myAnimation.Play("Punch");
		}
        else
        {
			myAnimation.Play("Punch2");
		}
	}
	public void Shooting()
	{
		if (tulpaFire != null && m_ChestCheck != null && timeToShootAgain > Random.Range(0.05f, 0.45f))
		{
			myAnimation.Play("Shoot");
			timeToShootAgain = 0;
			GameObject fireBall = Instantiate(tulpaFire, m_ChestCheck.position, Quaternion.identity) as GameObject;
			TulpaBullet bulletComponent = fireBall.GetComponent<TulpaBullet>();
			if (this.transform.localScale.x < 0f)
			{
				bulletComponent.direction = Vector2.left;
				bulletComponent.leftFace = true;
			}
			else
			{
				bulletComponent.direction = Vector2.right;
			}

		}
	}
	public void Move(float move, bool crouch, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, body.velocity.y);
			// And then smoothing it out and applying it to the character
			body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
			
			// If the input is moving the player right and the player is facing left...

			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
				
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
				
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			
			myAnimation.SetTrigger("jumping");
			// Add a vertical force to the player.
			m_Grounded = false;
			body.AddForce(new Vector2(0f, m_JumpForce));
		}
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// +


	IEnumerator WhaitGOTime(GameObject obj)
    {
		obj.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		obj.SetActive(false);
	}



}
