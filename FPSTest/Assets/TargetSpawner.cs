using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour {

	public float cooldown;
	
	float cooldownRemaining;
	
	public GameObject spawnTarget;
	
	GameObject spawnedGO;
	
	public bool isMele, isRanged;
	public GameObject aggroTarget;

	// Use this for initialization
	void Start () {
		if(!isMele && !isRanged){
			isMele = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnedGO == null){
			if(cooldownRemaining > 0){
				cooldownRemaining -= Time.deltaTime;
			}else{
				spawn ();
				cooldownRemaining = cooldown;
			}
		}
	}
	
	void spawn(){
		spawnedGO = (GameObject)GameObject.Instantiate(spawnTarget, this.transform.position, this.transform.rotation);
		if(aggroTarget != null && spawnedGO.GetComponent<EnemyPerformAttack>() != null && aggroTarget.GetComponent<EntityHealth>() != null){
			spawnedGO.GetComponent<EnemyPerformAttack>().isMele = this.isMele;
			spawnedGO.GetComponent<EnemyPerformAttack>().isRanged = this.isRanged;
			spawnedGO.GetComponent<EnemyPerformAttack>().target = aggroTarget.GetComponent<EntityHealth>();
		}
	}
	
}
