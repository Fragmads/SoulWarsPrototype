// InputCommand.cs
// Author : Fragmads
// Package : Network engine
// Last modification date : 03/09/2012
//
// InputCommand : A class that describe a set of input command sent by a player
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class InputCommand : MonoBehaviour  {
	
	// Properties
	//
	
	//public Player Player; 
	
	// Input status
	public float RightStickX = 0;
	public float RightStickY = 0;
	
	public bool RightStickDash = false;
	
	public float LeftStickX = 0;
	public float LeftStickY = 0;
	
	public bool L3 = false;
	
	public bool Attack = false;
	public bool Special = false;
	public bool Guard = false;
	public bool Jump = false;
	
	// When true, you will perform a LCancel when you land
	public bool LCancelWindow = false;
	// When true, you will perform a wall tech/ tech if you hit a Wall / ground
	public bool TechWindow = false;
	
	
	void FixedUpdate(){
		
		// TODO calculate if
		
		// TODO read commands from the controller
		// TODO do it in subclasses for each platforms !
		
	}
	
}
