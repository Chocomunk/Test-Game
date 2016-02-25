using UnityEngine;
using System.Collections;

public class LightningField : MonoBehaviour {

	public float enemyRestruckDelay = 5f;
	private bool triggered = false;
	private float currRange = 0;
	public float maxRange = 10;
	
	void Update(){
		if(!triggered && currRange < maxRange){
			this.currRange += Time.deltaTime*7;
		}else{
			Kill();
		}
		this.GetComponent<SphereCollider>().radius = currRange;
	}
	
	void OnTriggerEnter( Collider other ) {
		GameObject  e = other.gameObject;
		
		if( !e.CompareTag("Enemy") ) {  // It is not enemy
			print ("Is Enemy: " + e.name);
			
			return;
		}
		
		Hit h = e.GetComponent<Hit>();
		
		if( h == null ) { // the enemy is yet to be hit
			print ("Hitting: " + other.gameObject.name);
			
			//Call you lightning strike effect here
			
				
			//Create another copy of this lightning field, by doing this, it will start chaining when the condition is right
			Instantiate( gameObject, e.transform.position, Quaternion.identity );
			
			//Mark the enemy as hit
			h = e.AddComponent<Hit>();
			
			h.killDelay = enemyRestruckDelay;
			
			triggered = true;
		}
	}
	
	//Call this using an animation event, just in case the sphere strike nothing at all
	void Kill() {
		print ("Killing");
		Destroy(gameObject);
	}
}
