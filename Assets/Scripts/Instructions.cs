using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Instructions : MonoBehaviour {
	public float RequiredConfidence = 0.9f;
	public float GrabDistance = 2.0f;
	
	private Rigidbody _body;

	void Start () 
	{
		_body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		var done = false;
		if (MLHands.Right.KeyPoseConfidence >= RequiredConfidence && MLHands.Right.KeyPose == MLHandKeyPose.Pinch)
		{
			done = true;
		} 
		else if (MLHands.Left.KeyPoseConfidence >= RequiredConfidence && MLHands.Right.KeyPose == MLHandKeyPose.Pinch)
		{
			done = true;
		}

		if (done)
		{
			Destroy(gameObject);
			enabled = false;
			return;
		}
		
		Vector3 position = MLHands.Right.Center;
		_body.transform.position = position + Camera.main.transform.forward * GrabDistance;
		var targetRotation = Camera.main.transform.rotation.eulerAngles;
		targetRotation.x = 0;
		targetRotation.z = 0;
		_body.transform.rotation = Quaternion.Euler(targetRotation);
	}
}
