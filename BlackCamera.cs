using UnityEngine;
using System.Collections;

public class BlackCamera : MonoBehaviour
{
	private Camera myCamera;
	public bool timerincrease = true;
	public float timer = 0.0f;

void Start(){
		// set color of the panel transparent
		//GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0,0,0,0);
		
		//call GoBlack function after random 1-3 seconds 
		//Invoke("GoBlack",Random.Range(1,3));
			}

void FixedUpdate(){

		if (timerincrease) {
			timer += Time.fixedDeltaTime;
		} else {
			timer = 0.0f;
		}

		if(timer > 4.46f){
			GetComponent<Camera>().enabled=false;
			//myCamera.enabled=false;
		}
	}
}