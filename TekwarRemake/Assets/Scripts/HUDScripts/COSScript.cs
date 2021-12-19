using UnityEngine;
using System.Collections;

public class COSScript : MonoBehaviour {
	
	GameObject playerobj;
	float tmp;
	float cos;
	float cos_check;
	float cos_max;
	
	void Start()
	{
		cos_max = guiTexture.pixelInset.width;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
		if(playerobj == null)
			playerobj = GameObject.Find("Player(Clone)");
		else if(playerobj != null)
		{
			cos = playerobj.GetComponent<MainScript>().cos;
			if(cos_check != cos)
			{
			tmp = cos / 100 * cos_max;
			guiTexture.pixelInset = new Rect(
				guiTexture.pixelInset.x,
				guiTexture.pixelInset.y,
				tmp,
				guiTexture.pixelInset.height);
			}
			cos_check = playerobj.GetComponent<MainScript>().cos;
		}
	}
}