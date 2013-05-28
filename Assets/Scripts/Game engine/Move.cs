// Move.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Move : A move that can be executed by a fighter
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	
	// Properties
	//
		
	// Prefab of this move
	public GameObject Prefab;
	
	// Name of the animation of the Move
	public string AnimationName;
	
	// In number of frame
	public int Length = 15;
	private int FrameNumber = 0;
	
	// When true, the fighter may grab the ledge 
	public bool AutoEdgeGrab = false;
	// Even when he is not facing the ledge.
	public bool BlindAutoEdgeGrab = false;
	
	// Orientation of the move
	public enum Orientation{Up, Down, Forward, Back, Neutral};
	public Orientation orientation;
	
	//
	public bool isSpecial = false;
	
	// 
	public bool isEnded = false;
	
	// List of attacks performed during the move (some move are multi hits move)
	public List<Attack> Attacks;
	
	// Owner of this move
	public Fighter Owner;
	
	
	private int cleanFrame = 0;
	
	// Method
	//
	
		
	public void FixedUpdate(){
		
		this.FrameNumber ++;
		
		// If the move is finished, end it
		if(this.Length <= this.FrameNumber){
			this.EndMove();
		}
		
		// For each attack of this move
		foreach(Attack a in this.Attacks){
			
			// Check if it start or end
			if(a.StartFrame == this.FrameNumber){
				a.Owner = this.Owner;
				a.StartAttack();
				
			}
			else if(a.EndFrame == this.FrameNumber){
				
				a.EndAttack();
				
			}			
			
		} 
		
		// Destroy the move if it is ended
		if(this.isEnded){
			
			this.cleanFrame ++;
			
			if(this.cleanFrame > 2){
				
				GameObject.Destroy(this.gameObject);
				
			}
			
		}
		
		
	}
	
	
	// Called when the move End
	public void EndMove(){
		
		// Stop every attack of this move
		foreach(Attack a in this.Attacks){
			
			a.EndAttack();
			
		}
		
		this.isEnded = true;
		
		
		
		// Destroy the game object attached to this move
		//GameObject.Destroy(this.gameObject);
		
	}
	
}
