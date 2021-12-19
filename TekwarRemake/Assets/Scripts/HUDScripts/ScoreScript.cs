using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	GameObject playerobj;
	public int score;
	int score_check;
	string tmp = null;
	
	void Start()
	{
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
		if(playerobj == null)
			playerobj = GameObject.Find("Player(Clone)");
		else if(playerobj != null)
		{
			if(score_check != score)
			{
				guiText.text = score.ToString();
				if(guiText.text.Length < 7)
				{
					tmp = null;
					for(uint i = 0; i < 7-guiText.text.Length; i++)
						tmp += "0";
					guiText.text = tmp+guiText.text;
				}
			}
			score_check = score;
		}
	}
}
