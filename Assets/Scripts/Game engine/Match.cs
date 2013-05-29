// Match.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 04/09/2012
//
// Match : A match. This class is the logic who stop the game when one of the protagonist is defeated, or the time is running up, etc.
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Match : MonoBehaviour {
	
	// Properties
	//
				
	public List<Player> Players;
	
	
	public bool UseTimer= true;
	
	public bool Running = false;
	
	
	private float timeLeft = 840; // 840 sec =  8 min
	
	private enum GameStatus { Start, Running, Paused, End };
	private GameStatus Status;
		
	
	// Method
	//
	
	// Use this for initialization
	void Start () {
		
		
		this.Status = GameStatus.Start;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		// If the game is running
		if (this.Status == GameStatus.Running){
			
			this.Running = true;
			
			// If the game use a timer
			if (this.UseTimer){
				
				// We substract the time from last frame
				this.timeLeft = this.timeLeft - Time.deltaTime;
								
				
				// If there is no more time left
				if (this.timeLeft <= 0){
					
					// The game end
					this.Status = GameStatus.End;
									
				}	
				
				// TODO Update time HUD display
				
			}
			
		}
		// Else the game is paused
		else {
			
			this.Running = false;
			
		}
		
	}
}
