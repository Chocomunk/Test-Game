using UnityEngine;
using System.Collections;

public class EntityHealth : MonoBehaviour {
	
	public float maxHealth = 100;
	public float curHealth = 100;
	
	public float healthRegenRate = 5;
	public float healthRegenSpeed = 1;
	public float healthRegenStart = 5;
	
	public float barDisplay = 1;
	public float SizeScalar = 1;
	public float xchange = 10;
	public float maxPlayerDist = 20;
	
	float playerDist = 10000;
	float timestamp = 0.0f;
	
	public Vector2 rsize;
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	
	public Vector2 size = new Vector2(78.6f,24f);
	public Vector2 playerGUI = new Vector2(0,0);
	
	Vector3 pos;
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	public bool lookedAt = false;
	public bool dead = false;
	public bool canSee = false;
	
	bool isRegenHealth = false;
	
	public Renderer rend;
	
	public GameObject player;
	
	void Start(){
		rsize = size;
		rend = this.GetComponent<Renderer>();
		
		if(rend == null){
			rend = this.GetComponentInChildren<Renderer>();
		}
		
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update(){	
		//the player's health
		barDisplay = curHealth/maxHealth;
		
		playerDist = Vector3.Distance(this.transform.position, player.transform.position);
		
		if(isVisibleFrom(rend, Camera.main)){
			lookedAt = true;
		}else{
			lookedAt = false;
		}
		
		if(!dead && lookedAt && playerDist <= maxPlayerDist){
			canSee = true;
		}else{
			canSee = false;
		}
		
		if(curHealth < maxHealth && !isRegenHealth && Time.time > (timestamp + healthRegenStart) && this.gameObject == player) {
			StartCoroutine(RegainHealthOverTime());
		}else if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
	}
	
	void OnGUI(){
	// Check if not player
		if (this.gameObject != player) {

			pos = Camera.main.WorldToScreenPoint (this.GetComponent<MeshRenderer> ().transform.position);

//			pos.z /= SizeScalar;
//			rsize.x = size.x/pos.z;
//			rsize.y = size.y/pos.z;
			
			float posDisY = Screen.height / 2 - pos.y;
			float PosDisX = pos.x - size.x / 2 + xchange;
			pos.y = Screen.height / 2 + posDisY - rsize.y * 3;
			pos.x = PosDisX;
	// Check if is player
		} else {
			pos.y = playerGUI.y;
			pos.x = playerGUI.x;
			
			lookedAt = true;
		}
	// Check if visible	
		if (canSee) {
			//draw the background:
			GUI.BeginGroup (new Rect (pos.x, pos.y, rsize.x, rsize.y), emptyTex, progress_empty);
		
			//draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, rsize.x * barDisplay, rsize.y), progress_full);
			
			GUI.Box (new Rect (0, 0, rsize.x, rsize.y), fullTex, progress_full);
			
			GUI.EndGroup ();
			GUI.EndGroup ();
			
			GUI.Label (new Rect (pos.x + rsize.x/2 - GUI.skin.label.CalcSize(new GUIContent(curHealth + "/" + maxHealth)).x/2, pos.y + rsize.y / 4, rsize.x, rsize.y), curHealth + "/" + maxHealth, progress_full);		
		}	
	}
	
	public void RecieveDamage(float amt, GameObject Offender){
		curHealth -= amt;
		timestamp = Time.time;
		if(curHealth <= 0){
			this.GetComponent<PreformDeath>().Die(Offender);
			dead = true;
		}
	}
	
	public static bool isVisibleFrom(Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
	
	private IEnumerator RegainHealthOverTime() {
		isRegenHealth = true;
		while (curHealth < maxHealth) {
			curHealth += Mathf.Ceil(player.GetComponent<PlayerInfo>().health.healthRegenRate);
			yield return new WaitForSeconds(player.GetComponent<PlayerInfo>().health.healthRegenSpeed);
		}
		isRegenHealth = false;
	}
}
