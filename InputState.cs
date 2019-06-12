using UnityEngine;
using System.Collections;

public class InputState : MonoBehaviour {

	public bool actionbutton;
	public float absVelX = 0f;
	public float absVelY = 0f;
	public bool standing;
	public float standthres = 1f;

	private Rigidbody2D body2d;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		actionbutton = Input.anyKeyDown;
	}
	void FixedUpdate(){
		absVelX = System.Math.Abs (body2d.velocity.x);
		absVelY = System.Math.Abs (body2d.velocity.y);

		standing = absVelY <= standthres;

	}
}
