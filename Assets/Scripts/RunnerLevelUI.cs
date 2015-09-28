using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunnerLevelUI : MonoBehaviour 
{
	// To show coins number.
	public Text coinsText;	

	// Setter.
	public void SetCoinsText(int value)
	{
		coinsText.text = value.ToString();
	}
}
