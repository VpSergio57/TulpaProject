using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TulpaActions : MonoBehaviour {

	public TulpaController controller;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	private Animator myAnimation;
	public FloatingJoystick fixedJoystick;

	private void Awake()
	{

		myAnimation = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

			if (Input.GetButtonDown("Fire2"))
			{
				ShootingAction();
			}

			if (Input.GetButtonDown("Fire1"))
			{
				PunchingAction();
			}

			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}

		#elif UNITY_IOS || UNITY_ANDROID
					//horizontalMove = fixedJoystick.Horizontal * runSpeed;
					horizontalMove = SimpleInput.GetAxis("Horizontal") * runSpeed;

		#elif UNITY_WEBGL
					horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		#endif

		//Debug.Log(fixedJoystick.Horizontal);


		if (horizontalMove != 0)
        {
			myAnimation.SetBool("imWalking", true);
		}
        else
        {
			myAnimation.SetBool("imWalking", false);
		}






		/*if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
		*/
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	public void JumpAction()
    {
		jump = true;
	}

	public void ReloadAction()
    {
		SceneManager.LoadScene(0);
	}

	public void ShootingAction()
    {
		controller.Shooting();
	}
	public void PunchingAction()
	{
		controller.Punching();
	}
}
