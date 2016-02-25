using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DetermineMeleTargets : MonoBehaviour {
	
	Collider[] Entities;
	PlayerInfo info;
	
	// Use this for initialization
	void Start () {
		info = this.GetComponentInParent<PlayerInfo>();
		Debug.Log(LayerMask.LayerToName(8));
	}
	
	// Update is called once per frame
	void Update () {
		calcEntitiesInMeleRange();
	}
	
	void OnTriggerEnter(Collider useless){
		if(useless.gameObject.layer == 8){
			Destroy(useless.gameObject);
		}
		calcEntitiesInMeleRange();
		Debug.Log("Entered: " + useless.name);
	}
	
	void OnTriggerExit(Collider useless){
		calcEntitiesInMeleRange();
		Debug.Log("Left");
	}
	
	void calcEntitiesInMeleRange(){
		Entities = Physics.OverlapSphere(info.transform.position, info.meleRange);
		Collider[] entities = (Collider[])Entities.Clone();

		foreach(Collider entity in entities){
			if(entity.GetComponent<Renderer>() != null && EntityHealth.isVisibleFrom(entity.GetComponent<Renderer>(), Camera.main) && entity.GetComponent<EntityHealth>() != null && !info.attackableEntities.Contains(entity.GetComponent<EntityHealth>())){
				info.attackableEntities.Add(entity.GetComponent<EntityHealth>());
			}
		}

		EntityHealth[] entityHealths = info.attackableEntities.ToArray();

		foreach(EntityHealth entity in entityHealths){
			if(entity==null || (!entities.Contains(entity.GetComponent<Collider>()) || !EntityHealth.isVisibleFrom(entity.GetComponent<Renderer>(), Camera.main))){
				info.attackableEntities.Remove(entity);
			}
		}
	}
	
	public void removeEntity(EntityHealth entity){
		if(info.attackableEntities.Contains(entity)){
			Debug.Log("Removing "+entity.gameObject.name+" from mele targets");
			info.attackableEntities.Remove(entity);
		}
	}
}
