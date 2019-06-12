using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour {
	private float speed = 7.5f;
	private Vector3 movementVector;
	private CharacterController characterController;
	// Use this for initilization
	private Rigidbody2D body2d;
	public float timer = 0f;
	public float timer2 = 0f;
	public float timer3 = 0f;
	public bool timerincrease = false;
	public bool timerincrease2= false;
	public bool timerincrease3 = true;
	public AudioClip coinCollectSound;
	public AudioClip Goal;
	public float volume = 0.1f;
    public int q;
    public bool Thumbstick = true;
	public float lastVelocity = 0.0f;
	public float acceleration;


	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();

		StartCoroutine(MyLoadLevel(4.6f));
	}

	IEnumerator MyLoadLevel(float delay)
	{
		yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Application.LoadLevel("Brooks");
    }
	void Start () {

        q = PlayerPrefs.GetInt("q", q);


        characterController = GetComponent<CharacterController>();

	}
	// Update is called once per frame
	void Update () {

		if (timerincrease) {
			timer += Time.deltaTime;
		}

		if (timerincrease2) {
			timer2 += Time.deltaTime;
		}

		if (timerincrease3) {
			timer3 += Time.deltaTime;
		}
		

		if (timer3 > 0.1f && timer3 < 1.5f) {
			AudioSource.PlayClipAtPoint (coinCollectSound, transform.position, volume);
		}

		if (timer < 2.0f && timer2 >= 1.0f) {
			AudioSource.PlayClipAtPoint (Goal, transform.position, volume);
		}

		//if (timer3 >= 4.51f && timer3 <=4.55f) {
		//	Time.timeScale = 0;
		//}

		//if (timer3 <= 0.05f) {
		//	Time.timeScale = 0;
		//}

		if (Time.timeScale == 0) {
			timerincrease = false;
			timerincrease2 = false;
			timerincrease = false;
		}

		//Stops Time
		//if (timer3 > 10f && timer3 < 10.02f) {
		//	Time.timeScale = 0;
		//}

		if(Input.GetKeyDown(KeyCode.O)){
			Time.timeScale = 1;
		}



	//	movementVector.x = Input.GetAxis ("360_Joy");
	//	movementVector.x = Input.GetAxis ("360_JoyR");
//		movementVector.z = Input.GetAxis("LeftJoystickY") * speed;
//
//		if (Input.GetAxis ("360_Joy") > .5) {
//		if (Input.GetKey (KeyCode.UpArrow)) {
//			transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
//		}
//		if (Input.GetAxis ("360_Joy") > .5) {
//			if (Input.GetKey (KeyCode.UpArrow)) {
//				transform.position += new Vector3 (-speed * Time.deltaTime, 0.0f, 0.0f);
//		}
//		characterController.Move(movementVector * Time.deltaTime);
//	}
			}

	void FixedUpdate () 
	{

        if (Input.GetKeyDown(KeyCode.L))
        {
            q = 0;
            PlayerPrefs.SetInt("q", q);
        }

        //bool joyActiveR = Input.GetKey (KeyCode.RightArrow);
        //bool joyActiveL = Input.GetKey (KeyCode.LeftArrow);
        //Left Thumb Stick
        //bool joyActiveR = Input.GetAxis ("360_Joy") > .5;
        //bool joyActiveL = Input.GetAxis ("360_Joy") < -.5;
        //bool jetpackActive = Input.GetButton("Fire1");

        //Right Thumb Stick
        if (Thumbstick)
        {
            bool joyActiveR = Input.GetAxis("360_JoyR") > .5;
            bool joyActiveL = Input.GetAxis("360_JoyR") < -.5;
			float joypR = Input.GetAxis("360_JoyR");
			float joyRY = Input.GetAxis("360_JoyRY");
            if (joyActiveR)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
                transform.rotation = Quaternion.identity;
            }
            if (joyActiveL)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
                transform.rotation = Quaternion.identity;
            }

			//print(joyRY);

        }
        else
        {
            bool joyActiveR = Input.GetAxis("360_JoyL") > .5;
            bool joyActiveL = Input.GetAxis("360_JoyL") < -.5;
			float joypL = Input.GetAxis("360_JoyL");
            if (joyActiveR)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
                transform.rotation = Quaternion.identity;
            }
            if (joyActiveL)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
                transform.rotation = Quaternion.identity;
            }

			//print(joypL);
        }


		acceleration = (GetComponent<Rigidbody2D>().velocity.x - lastVelocity) / Time.fixedDeltaTime;
		lastVelocity = GetComponent<Rigidbody2D>().velocity.x;
		//print (lastVelocity);


		
	}


	void TimerGUI(){
		GUI.Box (new Rect (10, 10, 100, 25), "TIME " + timer.ToString("0.00"));
	}

	void Timer2GUI(){
		GUI.Box (new Rect (10, 45, 100, 25), "TIME " + timer2.ToString("0.00"));
	}
	void Timer3GUI(){
		GUI.Box (new Rect (10, 80, 100, 25), "TIME " + timer3.ToString("0.00"));
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "startBox"  && timer3 < 1.5f) {
            q = q + 1;
            PlayerPrefs.SetInt("q", q);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Application.LoadLevel("Brooks");
		}
		//if (timer > 2.0f) {
          //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Application.LoadLevel("Brooks");
        //}
		if (collider.gameObject.tag == "startBox") {
			timerincrease = true;
		}
	
		if (collider.gameObject.tag == "stopBox") {
			timerincrease=true;
			timerincrease2=false;
			timer2=0;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.tag == "stopBox") {
			timerincrease = false;
			timerincrease2 = true;
		}
	}

    void FStartGUI()
    {
        GUI.Box(new Rect(10, 150, 100, 25), "FStart " + q.ToString("0"));
    }

    void OnGUI(){
		//TimerGUI ();
		//Timer2GUI ();
		//Timer3GUI ();
        //FStartGUI();
	}

	}
