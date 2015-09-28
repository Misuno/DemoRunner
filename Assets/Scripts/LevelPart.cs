using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPart : MonoBehaviour 
{
	public PartType type;

	// We need this to position other parts.
	public int length = 1;

	// My geometry.
	public Floor	floorAssets;
	public Wall 	leftWall;
	public Wall 	rightWall;
	public Wall 	ceiling;

	// Parts combining rules.
	public List<PartType> canConnectTo;
	
}
