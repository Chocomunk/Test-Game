using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public float killDelay = 3f;
	
	void Update(){
		if(killDelay <= 0f){
			Destroy(this);
		}else{
			killDelay -= Time.deltaTime;
		}
	}
}
