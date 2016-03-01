using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	
	PlayerInfo info;
	
	Texture2D GUIBack;
	Texture2D StatsMenu;
	Texture2D ShopMenu;
	
	GUIStyle GameGui;
	
	Vector2 bigPos = new Vector2(Screen.width/8, Screen.height/10);
	Vector2 bigSize = new Vector2(6*Screen.width/8, 8*Screen.height/10);
	
	public int ItemShopPageNums = 2;
	
	int CurrentShopPage = 1;

	bool isInMenu = false;

	// Use this for initialization
	void Start () {
		info = this.GetComponent<PlayerInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		if(info.isInMenu != isInMenu){
			if(isInMenu){
				Cursor.visible = true;
				this.GetComponentInChildren<Camera>().GetComponent<MouseLook>().enabled = false;
				this.GetComponent<MouseLook>().enabled = false;
			} else {
				Cursor.visible = false;
				this.GetComponentInChildren<Camera>().GetComponent<MouseLook>().enabled = true;
				this.GetComponent<MouseLook>().enabled = true;
			}
			info.isInMenu = isInMenu;
		}
	}
	
	void OnGUI(){
		
		if(GameGui == null){
			GameGui = GUI.skin.label;
		}
		
	//Big Stats Menu
		if(Input.GetButton("Stats Menu")){
			BigStatsMenu();
			isInMenu = false;
		
	//Item Shop
		}else if(Input.GetButton("Item Shop")){
			ItemShop();
			isInMenu = true;
			
	//Small Stats Menu
		}else{
			SmallStatsMenu();
			isInMenu = false;
		}
	}

	
//Big Stats Menu
#region BigStatsMenu
	void BigStatsMenu(){
		//Stats Big
		GUI.BeginGroup(new Rect(bigPos.x, bigPos.y, bigSize.x, bigSize.y));
		
		//Background box
		GUI.Box(new Rect(0, 0, bigSize.x, bigSize.y), StatsMenu);
		
		//Mele
		BigStatsMele();
		
		//Ranged
		BigStatsRanged();
		
		GUI.EndGroup();
	}
	
	void BigStatsMele(){
		//Weapon Type label
		GUI.Label(new Rect(bigSize.x/4 - GameGui.CalcSize(new GUIContent("Mele")).x/2, 10, bigSize.x/2 - 10, 25), "Mele");
		
		//Range
		GUI.Label(new Rect(10, 50, bigSize.x/2 - 10, 20), "Mele Range: " + info.meleRange);
		
		//Cooldown
		GUI.Label(new Rect(10, 70, bigSize.x/2 - 10, 20), "Mele Cooldown: " + string.Format("{0:0.00}", info.meleCooldown));
		
		//Damage
		GUI.Label(new Rect(10, 90, bigSize.x/2 - 10, 20), "Mele Damage: " + info.meleDamage);
	}
	
	void BigStatsRanged(){
		//Weapon Type label
		GUI.Label(new Rect(3*bigSize.x/4 - GameGui.CalcSize(new GUIContent("Ranged")).x/2, 10, bigSize.x/2 - 10, 25), "Ranged");
		
		//Range
		GUI.Label(new Rect(bigSize.x/2, 50, bigSize.x - 10, 20), "Ranged Range: " + info.rangedRange);
		
		//Cooldown
		GUI.Label(new Rect(bigSize.x/2, 70, bigSize.x - 10, 20), "Ranged Cooldown: " + string.Format("{0:0.00}", info.rangedCooldown));
		
		//Damage
		GUI.Label(new Rect(bigSize.x/2, 90, bigSize.x - 10, 20), "Ranged Damage: " + info.rangedDamage);
		
		//Gold counter
		GUI.Label(new Rect(bigSize.x/2 - GameGui.CalcSize(new GUIContent("Gold: " + info.GoldIncome)).x/2, bigSize.y - 30, bigSize.x, bigSize.y), "Gold: " + info.GoldIncome);
	}
#endregion
	
//Small Stats Menu
#region SmallStatsMenu

	void SmallStatsMenu(){
	//Stats small
		GUI.BeginGroup(new Rect(Screen.width - 175, 0, 175, 225));
		
	//Background box
		GUI.Box(new Rect(0, 0, 175, 225), StatsMenu);
		
	//Mele
		SmallStatsMele();
		
	//Ranged
		SmallStatsRanged();
		
	//Gold	
		//Gold counter
		GUI.Label(new Rect(175/2 - GameGui.CalcSize(new GUIContent("Gold: " + info.GoldIncome)).x/2, 200, 150, 225), "Gold: " + info.GoldIncome);
		
		GUI.EndGroup();
	}
	
	void SmallStatsMele(){
		//Range
		GUI.Label(new Rect(10, 10, 150, 25), "Mele Range: " + info.meleRange);
		
		//Cooldown
		GUI.Label(new Rect(10, 35, 150, 50), "Mele Cooldown: " + string.Format("{0:0.00}", info.meleCooldown));
		
		//Damage
		GUI.Label(new Rect(10, 60, 150, 75), "Mele Damage: " + info.meleDamage);
	}
	
	void SmallStatsRanged(){
		//Range
		GUI.Label(new Rect(10, 105, 150, 100), "Ranged Range: " + info.rangedRange);
		
		//Cooldown
		GUI.Label(new Rect(10, 130, 150, 125), "Ranged Cooldown: " + string.Format("{0:0.00}", info.rangedCooldown));
		
		//Damage
		GUI.Label(new Rect(10, 155, 150, 150), "Ranged Damage: " + info.rangedDamage);
	}
	
#endregion

//Item Shop
#region ItemShop

	void ItemShop(){
	//Item Shop
		GUI.BeginGroup(new Rect(bigPos.x, bigPos.y, bigSize.x, bigSize.y));
		
	//Background box
		GUI.Box(new Rect(0, 0, bigSize.x, bigSize.y), GUIBack);
		
		this.gameObject.SendMessage("ItemShopPage" + CurrentShopPage.ToString());
		
	//Gold	
		//Gold counter
		GUI.Label(new Rect(bigSize.x/2 - GameGui.CalcSize(new GUIContent("Gold: " + info.GoldIncome)).x/2, bigSize.y - 30, bigSize.x, 30), "Gold: " + info.GoldIncome);
	//Pages
		//Last
		if(CurrentShopPage > 1) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(30, bigSize.y - 30, GameGui.CalcSize(new GUIContent("Last")).x, 25), "Last")){CurrentShopPage--;}
		GUI.enabled = true;
		
		//Next
		if(CurrentShopPage < ItemShopPageNums) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x -GameGui.CalcSize(new GUIContent("Next")).x - 30, bigSize.y - 30, GameGui.CalcSize(new GUIContent("Next" + info.GoldIncome)).x, 25), "Next") && CurrentShopPage <= ItemShopPageNums){CurrentShopPage++;}
		GUI.enabled = true;
		
		GUI.EndGroup();
	}

//Pages
	void ItemShopPage1(){
		//Mele
		ItemShopMele();
		
		//Ranged
		ItemShopRanged();
		
		//Health
		ItemShopHealth();
	}
	void ItemShopPage2(){
		//Mele
		BigStatsMele();
		
		//Ranged
		BigStatsRanged();
	}
	
//Mele
	void ItemShopMele(){
		
	//Label
		GUI.Label(new Rect(bigSize.x/4 - GameGui.CalcSize(new GUIContent("Mele")).x/2, 10, bigSize.x/2 - 15, 20), "Mele");
		
	//Small
		//Increase Range
		if(info.GoldIncome >= 5) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 30, bigSize.x/2 - 15, 40), "+5 range (10 gold)")){
			info.GoldIncome -= 10;
			info.meleRange += 5;
		}
		GUI.enabled = true;
		
		//Decrease Cooldown 
		if(info.GoldIncome >= 5 && info.meleCooldown > 0.3) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 75, bigSize.x/2 - 15, 40), "-.1 cool (10 gold, min 0.2)")){
			info.GoldIncome -= 10;
			info.meleCooldown -= 0.1f;
		}
		GUI.enabled = true;
		
		//Increase Damage
		if(info.GoldIncome >= 5) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 120, bigSize.x/2 - 15, 40), "+5 damage (10 gold)")){
			info.GoldIncome -= 10;
			info.meleDamage += 5;
		}
		GUI.enabled = true;
		
	//Medium
		//Increase Range
		if(info.GoldIncome >= 20) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 180, bigSize.x/2 - 15, 40), "+20 range (20 gold)")){
			info.GoldIncome -= 20;
			info.meleRange += 20;
		}
		GUI.enabled = true;
		
		//Decrease Cooldown 
		if(info.GoldIncome >= 20 && info.meleCooldown > 0.6) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 225, bigSize.x/2 - 15, 40), "-.4 cool (20 gold, min 0.2)")){
			info.GoldIncome -= 20;
			info.meleCooldown -= 0.4f;
		}
		GUI.enabled = true;
		
		//Increase Damage
		if(info.GoldIncome >= 20) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 270, bigSize.x/2 - 15, 40), "+20 damage (20 gold)")){
			info.GoldIncome -= 20;
			info.meleDamage += 20;
		}
		GUI.enabled = true;
	}
	
//Ranged
	void ItemShopRanged(){
	//Label
		GUI.Label(new Rect(3*bigSize.x/4 - GameGui.CalcSize(new GUIContent("Ranged")).x/2, 10, bigSize.x/2 - 15, 20), "Ranged");
		
	//Small
		//Increase Range
		if(info.GoldIncome >= 5) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 30, bigSize.x/2 - 10, 40), "+5 range (5 gold)")){
			info.GoldIncome -= 5;
			info.rangedRange += 5;
		}
		GUI.enabled = true;
		
		//Decrease Cooldown 
		if(info.GoldIncome >= 5 && info.rangedCooldown > 0.06) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 75, bigSize.x/2 - 10, 40), "-.01 cool (5 gold, min 0.05)")){
			info.GoldIncome -= 5;
			info.rangedCooldown -= 0.01f;
		}
		GUI.enabled = true;
		
		//Increase Damage
		if(info.GoldIncome >= 5) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 120, bigSize.x/2 - 10, 40), "+5 damage (5 gold)")){
			info.GoldIncome -= 5;
			info.rangedDamage += 5;
		}
		GUI.enabled = true;
		
	//Medium
		//Increase Range
		if(info.GoldIncome >= 20) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 180, bigSize.x/2 - 10, 40), "+20 range (10 gold)")){
			info.GoldIncome -= 10;
			info.rangedRange += 20;
		}
		GUI.enabled = true;
		
		//Decrease Cooldown 
		if(info.GoldIncome >= 20 && info.rangedCooldown > 0.08) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 225, bigSize.x/2 - 10, 40), "-.04 cool (10 gold, min 0.05)")){
			info.GoldIncome -= 10;
			info.rangedCooldown -= 0.04f;
		}
		GUI.enabled = true;
		
		//Increase Damage
		if(info.GoldIncome >= 20) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 270, bigSize.x/2 - 10, 40), "+20 damage (10 gold)")){
			info.GoldIncome -= 10;
			info.rangedDamage += 20;
		}
		GUI.enabled = true;
	}
	
//Health
	void ItemShopHealth(){
	//Small
		//Increases max health
		if(info.GoldIncome >= 10) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(10, 315, bigSize.x/2 - 15, 40), "+1 HP")){
			info.GoldIncome -= 10;
			info.health.maxHealth += 1;
			info.health.curHealth += 1;
		}
		GUI.enabled = true;
		
	//Medium
		//Increases max health
		if(info.GoldIncome >= 20) {GUI.enabled = true;} else {GUI.enabled = false;}
		if(GUI.Button(new Rect(bigSize.x/2, 315, bigSize.x/2 - 10, 40), "+5 HP")){
			info.GoldIncome -= 10;
			info.health.maxHealth += 5;
			info.health.curHealth += 5;
		}
		GUI.enabled = true;
	}
	
#endregion
	
}
