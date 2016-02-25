using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {

	public int GoldIncome;
	
	public EntityHealth health;
	public PlayerGUI thisGUI;
	public PerformsAttack thisAttack;
	public DetermineMeleTargets meleDetermination;
	
	public SphereCollider meleCollider;
	public List<EntityHealth> attackableEntities;
	
//Mele stats
	//Base stats
	public float baseMeleRange = 2.0f;
	public float baseMeleCooldown = 1f;
	public float baseMeleDamage = 50f;
	
	//Changeable stats
	public float meleRange;
	public float meleCooldown;
	public float meleDamage;
	
	float oldMeleRange;
	
//Ranged stats
	//Base stats
	public float baseRangedRange = 10f;
	public float baseRangedCooldown = 0.5f;
	public float baseRangedDamage = 25f;
	
	//Changeable stats
	public float rangedRange;
	public float rangedCooldown;
	public float rangedDamage;
	
	// Use this for initialization
	void Start () {
		meleRange = baseMeleRange;
		rangedRange = baseRangedRange;
		
		meleCooldown = baseMeleCooldown;
		rangedCooldown = baseRangedCooldown;
		
		meleDamage = baseMeleDamage;
		rangedDamage = baseRangedDamage;
		
		health = this.GetComponent<EntityHealth>();
		thisGUI = this.GetComponent<PlayerGUI>();
		thisAttack = this.GetComponent<PerformsAttack>();
		meleDetermination = this.GetComponentInChildren<DetermineMeleTargets>();

		attackableEntities = new List<EntityHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if(oldMeleRange != meleRange){
			updateColliders();
			oldMeleRange = meleRange;
		}
	}
	
	public void giveGold(int goldReturn){
		GoldIncome += goldReturn;
	}
	
	public void updateColliders(){
		meleCollider.radius = this.meleRange;
	}
	
}
