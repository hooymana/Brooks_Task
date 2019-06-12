using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CursorPositionsY : MonoBehaviour {

	public int x;
	public int z1;
	private string fileName = "C:/Users/Public/Documents/Unity Projects/SuperD2D/CursorPosY/position.txt"; // file pathname
	public float interval = 0.01f; // save positions each 0.01 second
	public float tSample = 1.0f; // sampling starts after this time
	public float timer = 0f;

	private List<float> positions;
	
	void Start(){
		positions = new List<float>(); // initialize the array...
		// and start recording after tSample:
		InvokeRepeating("RecPoint", tSample, interval);

		z1=PlayerPrefs.GetInt("z1",z1);
	}

	void RecPoint(){
		positions.Add(Input.GetAxis("360_JoyRY")); // store position...
	}
	
	// function that saves to a text file:
	void SaveToFile(string fileName){
		System.IO.File.WriteAllText(fileName, ""); // clear old file, if any
		foreach (float pos in positions){
			// format XYZ separated by ; and with 2 decimal places:
			string line = System.String.Format("{0,3:f2}\r\n", pos);
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

		if(Input.GetKeyDown(KeyCode.I)){
			PlayerPrefs.SetInt ("z1",0);
		}

		if (timer >= 300.51 && timer <= 300.53) {

			//Time.timeScale = 0;

		//	if (y < 200) {
				z1 = z1 + 1;
		//	} else {
		//		y = 0;
		//	}
			PlayerPrefs.SetInt ("z1", z1);
			x = z1;
			CancelInvoke ("RecPoint");
			SaveToFile (fileName + x);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			z1=0;
			PlayerPrefs.SetInt ("z1", z1);
		}
		
		

		//if (timer >= 2.0) {
		//	Destroy(gameObject);
		//}

	}

}