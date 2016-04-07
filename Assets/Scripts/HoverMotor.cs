using UnityEngine;
using System.Collections;

public class HoverMotor : MonoBehaviour {

	public float speed = 90f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;

	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidBody;

	void Awake () {

		carRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		powerInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

	//Moving in physics int to fixedUpdate steps than frame rate; 
	void FixedUpdate(){
		//Ray(casting position from craft, direction = -ve transform.up
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		//checking we are with in our hover height of ground and if we are pusing up.
		if(Physics.Raycast(ray, out hit, hoverHeight)){
			float propotionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * propotionalHeight * hoverForce;
			carRigidBody.AddForce (appliedHoverForce, ForceMode.Acceleration);
		}

		carRigidBody.AddRelativeForce (0f, 0f, powerInput * speed);
		carRigidBody.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
	}

}
