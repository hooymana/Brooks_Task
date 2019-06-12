using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class RecVel : MonoBehaviour {
	
	public int x;
	public int y2;
	private string fileName = "C:/Users/Public/Documents/Unity Projects/SuperD2D/RecVel/position.txt"; // file pathname
	public float interval = 0.01f; // save positions each 0.01 second
	public float tSample = 1.0f; // sampling starts after this time
	public float timer = 0f;
	public Vector2 lastVelocity;
	public Vector2 acceleration;
	private List<Vector2> positions;
	
	void Start(){
		positions = new List<Vector2>(); // initialize the array...
		// and start recording after tSample:
		InvokeRepeating("RecPoint", tSample, interval);
		
		y2=PlayerPrefs.GetInt("y2",y2);
	}
	
	void RecPoint(){
		positions.Add(lastVelocity); // store position...
	}
	
	// function that saves to a text file:
	void SaveToFile(string fileName){
		System.IO.File.WriteAllText(fileName, ""); // clear old file, if any
		foreach (Vector3 pos in positions){
			// format XYZ separated by ; and with 2 decimal places:
			string line = System.String.Format("{0,3:f2}\r\n", pos.x);
			System.IO.File.AppendAllText(fileName, line); // append to the file
		}
	}
	
	
	// example of use:
	//void OnGUI(){
	//	if (GUI.Button (new Rect (10, 100, 120, 30), "Save")) {
	//		CancelInvoke ("RecPoint"); // stop recording
	//		SaveToFile (fileName + x); // save positions
	//	}
	//}
	//void OnTriggerEnter2D(Collider2D target){
	//		if (target.gameObject.tag == "Wall")
	//x++;
	//		CancelInvoke ("RecPoint");
	//		SaveToFile (fileName + x);
	
	//	}
	
	
	void FixedUpdate(){
		timer += Time.deltaTime;
		
		acceleration = (GetComponent<Rigidbody2D>().velocity - lastVelocity) / Time.fixedDeltaTime;
		lastVelocity = GetComponent<Rigidbody2D>().velocity;
		//print (lastVelocity);
		
		if(Input.GetKeyDown(KeyCode.I)){
			PlayerPrefs.SetInt ("y2",0);
		}

		//positions.Add(lastVelocity); // store position...
		
		if (timer >= 4.51 && timer <= 4.52) {
			
			//Time.timeScale = 0;
			
			//	if (y < 200) {
			y2 = y2 + 1;
			//	} else {
			//		y = 0;
			//	}
			
			PlayerPrefs.SetInt ("y2", y2);
			
			x = y2;
			CancelInvoke ("RecPoint");
			SaveToFile (fileName + x);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			y2=0;
			PlayerPrefs.SetInt ("y2", y2);
		}
		
		
		
		//if (timer >= 2.0) {
		//	Destroy(gameObject);
		//}
		
	}
	
	void TrialGUI(){
	//	GUI.Box (new Rect (10, 120, 100, 25), "Trial " + y1.ToString("0"));
	}
	
	void OnGUI(){
		//TrialGUI ();
	}
	
}