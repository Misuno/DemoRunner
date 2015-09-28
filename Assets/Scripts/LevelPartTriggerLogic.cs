using UnityEngine;
using System.Collections;

public class LevelPartTriggerLogic : MonoBehaviour 
{
	// If we exit one level part, we should generate new one.
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			RunnerLevelLogic.Instance.GenerateLevelParts(1);
		}
	}
}
