using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int value;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			RunnerLevelLogic.Instance.PlayerGotCoinWithValue(value);
			Destroy(gameObject);
		}
	}

}
