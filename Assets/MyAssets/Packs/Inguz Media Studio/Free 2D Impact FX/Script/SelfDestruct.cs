using UnityEngine;

namespace MyAssets.Packs.Inguz_Media_Studio.Free_2D_Impact_FX.Script
{


public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.

	void Start () {
		if ( selfdestruct_in != 0){ 
			Destroy (gameObject, selfdestruct_in);
		}
	}
}


}
