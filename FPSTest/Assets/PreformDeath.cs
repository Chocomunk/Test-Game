using UnityEngine;
using System.Collections;

public class PreformDeath : MonoBehaviour {

	public int GoldReturn = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Die(GameObject Killer){
		if(Killer.GetComponent<PlayerInfo>() != null){
			Debug.Log("Killer has info");
			Killer.GetComponent<PlayerInfo>().meleDetermination.removeEntity(this.GetComponent<EntityHealth>());
			giveLoot(Killer);
		}
		Debug.Log("Destroying "+this.gameObject.name);
		Destroy(this.gameObject);
	}
	
	void giveLoot(GameObject Killer){
		Killer.GetComponent<PlayerInfo>().giveGold(GoldReturn);
	}
}
