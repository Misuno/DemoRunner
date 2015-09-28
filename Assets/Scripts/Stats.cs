using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	// Singleton.
	public static Stats Instance;

	// Results.
	public int coins;
	public int currentBest;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}

	// Don't reset top result.
	public void Reset()
	{
		coins = 0;
	}

	/// <summary>
	/// Checks if new score is higher than current record.
	/// If so, updates record value
	/// </summary>
	/// <returns><c>true</c>, if best score was beaten, <c>false</c> otherwise.</returns>
	public bool UpdateAndCheckBest()
	{
		if (coins > currentBest)
		{
			currentBest = coins;
			return true;
		}

		return false;
	}
}
