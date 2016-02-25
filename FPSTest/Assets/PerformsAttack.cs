using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerformsAttack : MonoBehaviour {
	
	float meleCooldownremaining = 0.0f;
	float meleCooldownRatio;
	
	float rangedCooldownremaining = 0.0f;
	float rangedCooldownRatio;
	
	public GameObject debrisPrefab;
	
	public Vector2 size = new Vector2(165f, 25f);
	public Vector2 cooldownBarGUI = new Vector2(0,30);
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	EntityHealth[] tempEntities;
	
	Vector2 pos;
	
	Vector3 hitPoint;
	
	PlayerInfo info;

	// Use this for initialization
	void Start () {
		info = this.GetComponent<PlayerInfo>();
		pos = cooldownBarGUI;
	}
	
	// Update is called once per frame
	void Update () {
		if (meleCooldownremaining > 0){
			meleCooldownremaining -= Time.deltaTime;
		}
		if (meleCooldownremaining < 0.012){
			meleCooldownremaining = 0;
		}
		
		if (rangedCooldownremaining > 0){
			rangedCooldownremaining -= Time.deltaTime;
		}
		if (rangedCooldownremaining < 0.012){
			rangedCooldownremaining = 0;
		}
		
		meleCooldownRatio = (info.meleCooldown - meleCooldownremaining)/info.meleCooldown;
		rangedCooldownRatio = (info.rangedCooldown - rangedCooldownremaining)/info.rangedCooldown;
	
//		Mele
		if(Input.GetButtonDown("Mele Attack") && meleCooldownremaining <= 0){
			meleAttack(info.meleDamage);
			meleCooldownremaining = info.meleCooldown;
		}
	
//		Ranged
		if(Input.GetMouseButtonDown(0) && rangedCooldownremaining <= 0){
			rangedAttack(info.rangedRange, info.rangedDamage);
			rangedCooldownremaining = info.rangedCooldown;
		}

	}
	
	void OnGUI(){
		
//		Mele Cooldown Bar
		//Text inside
		string meleString = string.Format("{0:0.00}", info.meleCooldown - meleCooldownremaining) + "/" + string.Format("{0:0.0}", info.meleCooldown) + " sec";

		//draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);
		
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * meleCooldownRatio, size.y), progress_full);
		
		GUI.Box (new Rect (0, 0, size.x, size.y), fullTex, progress_full);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
		
		GUI.Label (new Rect (pos.x + size.x/2  - GUI.skin.label.CalcSize(new GUIContent(meleString)).x, pos.y + size.y / 4, size.x, size.y), meleString, progress_full);	

//		Ranged Cooldown Bar
		//Text inside
		string rangedString = string.Format("{0:0.00}", info.rangedCooldown - rangedCooldownremaining) + "/" + string.Format("{0:0.00}", info.rangedCooldown) + " sec";

		//draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y + size.y + 5, size.x, size.y), emptyTex, progress_empty);
		
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * rangedCooldownRatio, size.y), progress_full);
		
		GUI.Box (new Rect (0, 0, size.x, size.y), fullTex, progress_full);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
		
		GUI.Label (new Rect (pos.x + size.x/2 - GUI.skin.label.CalcSize(new GUIContent(rangedString)).x, pos.y + 5 + 5*size.y / 4, size.x, size.y), rangedString, progress_full);
		
	}
	
	void rangedAttack(float range, float damage){
	
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hitInfo;
		
		if(Physics.Raycast (ray, out hitInfo, range)){
			
			hitPoint = hitInfo.point;
			GameObject go = hitInfo.collider.gameObject;
			
			Debug.Log ("Hit Object: " + go.name);
			Debug.Log ("Hit Point: "  + hitPoint);
			
			EntityHealth h = go.GetComponent<EntityHealth>();
			
			if(h != null){
				h.RecieveDamage(damage, this.gameObject);
			}
			
			if(debrisPrefab != null){
				Instantiate (debrisPrefab, hitPoint, Quaternion.identity);
				hitPoint = Vector3.forward*100;
			}
			
		}	
	}
	
	void meleAttack(float damage){
		tempEntities = info.attackableEntities.ToArray();
		foreach(EntityHealth entity in tempEntities){
			entity.RecieveDamage(damage, this.gameObject);
		}
	}
	
}
