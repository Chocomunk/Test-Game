using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPerformAttack : MonoBehaviour {
	
	public float meleCooldown = 1.5f;
	float meleCooldownremaining;
	
	public float rangedCooldown = 0.5f;
	float rangedCooldownremaining;
	
	public float meleDamage = 10;
	public float rangedDamage = 5;
	
	public float baseMeleRange = 2.0f;
	public float baseRangedRange = 10.0f;
	
	public float meleRange = 2.0f;
	public float rangedRange = 10.0f;
	
	public GameObject debrisPrefab;
	
	public bool isMele, isRanged;
	bool canMele, canRanged;
	bool firstMele, firstRanged = false;
	
	public EntityHealth target;
	
	// Use this for initialization
	void Start () {
		meleCooldownremaining = meleCooldown;
		rangedCooldownremaining = rangedCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		if (meleCooldownremaining > 0){
			meleCooldownremaining -= Time.deltaTime;
		}
//		if (meleCooldownremaining < 0.012){
//			meleCooldownremaining = 0;
//		}
		
		if (rangedCooldownremaining > 0){
			rangedCooldownremaining -= Time.deltaTime;
		}
		if (rangedCooldownremaining < 0.012){
			rangedCooldownremaining = 0;
		}
		
		if(target != null){
		//	Mele
			if(firstMele == false){
				meleRange = 7*baseMeleRange/8;
				if(Vector3.Distance(target.transform.position, this.transform.position) <= meleRange){
					firstMele = true;
				}
			}else{
				meleRange = baseMeleRange;
				if(Vector3.Distance(target.transform.position, this.transform.position) >= meleRange){
					firstMele = false;
				}
			}
			if(meleCooldownremaining <= 0 && Vector3.Distance(target.transform.position, this.transform.position) <= meleRange && isMele){
				canMele = true;
			}else{
				canMele = false;
			}
			
			if(canMele){
				meleCooldownremaining = meleCooldown;
				attack(meleDamage);
			}
			
		//	Ranged
			if(firstRanged == false){
				rangedRange = baseRangedRange/8;
				firstRanged = true;
			}else{
				rangedRange = baseRangedRange;
				if(Vector3.Distance(target.transform.position, this.transform.position) <= rangedRange){
					firstRanged = false;
				}
			}
			if(rangedCooldownremaining <= 0 && Vector3.Distance(target.transform.position, this.transform.position) <= rangedRange && isRanged){
				canRanged = true;
			}
			
			if(canRanged){
				rangedCooldownremaining = rangedCooldown;
				attack(rangedDamage);
				
				if(debrisPrefab != null){
					Instantiate (debrisPrefab, Vector3.forward, Quaternion.identity);
				}
			}
		}
			
	}
	
	void attack(float damage){	
		if(target != null){
			target.RecieveDamage(damage, this.gameObject);
		}
	}
}
