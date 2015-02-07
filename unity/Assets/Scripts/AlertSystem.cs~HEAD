using UnityEngine;
using System.Collections;

public class AlertSystem : MonoBehaviour {

	public Light mainLight;
	
	public const int STATUS_CLEAR = 0;
	public const int STATUS_CAUTION = 1;
	public const int STATUS_ALERT = 2;
	
	int currentStatus = STATUS_CLEAR;
	Color targetColor = Color.red;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentStatus){
			case STATUS_CLEAR:
				break;
			case STATUS_CAUTION:
				break;
			case STATUS_ALERT:
				AlertLight();
				break;
		}
	}
	
	void AlertLight(){
		if(mainLight.color != targetColor){
			mainLight.color = Color.Lerp(mainLight.color, targetColor, 0.3f);
		}else{
			if(targetColor == Color.red){
				targetColor = Color.white;
			}else{
				targetColor = Color.red;
			}
		}
	}
	
	public void Alert(){
		currentStatus = STATUS_ALERT;
	}
	
	public int GetStatus(){
		return currentStatus;
	}
}
