// Player.cs
// Author : Fragmads
// Package : Network engine
// Last modification date : 03/09/2012
//
// Player : A player, playing a character in the game
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Match CurrentMatch;
	
	public Fighter fighter;
	
	public InputCommand InputCommand;
	
	public int PlayerNumber = 0;
	
	public bool isLocal = true;
	
	public int LifeRemaining = 4;
	
	// Use this for initialization
	void Start () {
		
		
		
	}
	
	
	public void LooseLife(){
		
		// Loose one life
		this.LifeRemaining --;
		
		// If it's the last life of this player
		if(this.LifeRemaining <= 0){
			
			// TODO GameEnd
			
		}
		else{
			
			this.ReSpawn();
			
		}		
		
	}
	
	public void ReSpawn(){
		
		// Find the SpawnPoint
		SpawnPoint spawn = null;
		
		// Foreach spawn point in this scene
		foreach(SpawnPoint sp in SpawnPoint.StageSpawnPoint){
			
			// Find the one dedicated to this player
			if(sp.PlayerSpot == this.PlayerNumber){
				spawn = sp;
				break;
			}
			
		}
		
		if(spawn != null){
			// Move the fighter to his Spawn Position
			this.fighter.gameObject.transform.position = spawn.gameObject.transform.position;
			
			// Reset the HP
			this.fighter.CurrentHp = this.fighter.MaxHp;
			
			// Clean all States from the fighter
			foreach(AFighterState af in this.fighter.gameObject.GetComponents<AFighterState>()){
				
				GameObject.Destroy(af);
				
			}
			
			// The fighter is now airborne
			Airborne airborne = this.fighter.gameObject.AddComponent<Airborne>();
			this.fighter.State = airborne;
		}
		else{
			Debug.Log("Spawn not found !");
		}
		
	}
	
	
}
