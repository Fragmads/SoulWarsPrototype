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

public abstract class Move : MonoBehaviour {
	
	// Properties
	//
		
	// In number of frame
	public int Length = 15;
	
	// When true, the fighter may grab the ledge 
	public bool AutoEdgeGrab = false;
	// Even when he is not facing the ledge.
	public bool BlindAutoEdgeGrab = false;
	
	// Orientation of the move
	public enum Orientation{Up, Down, Forward, Back, Neutral};
	public Orientation orientation;
	
	public bool isSpecial = false;
	
	
	// Method
	//
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void FixedUpdate(){
		
		this.Length --;
		
		// If the move is finished, end it
		if(this.Length <= 0){
			this.EndMove();
		}
		
	}
	
	// Called when the move End
	public abstract void EndMove();
	
}
