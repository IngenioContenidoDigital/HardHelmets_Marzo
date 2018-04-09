#pragma strict
var text_fx_name : TextMesh;
var fx_prefabs : GameObject[];
var index_fx : int = 0;


private var ray : Ray;
private var ray_cast_hit : RaycastHit;


function Start () {
	text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;
	yield WaitForSeconds(6.5);
	Destroy(GameObject.Find("Instructions"));
}


function Update () {
	if ( Input.GetMouseButtonDown(0) ){
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, ray_cast_hit)){			
			var aux = Instantiate(fx_prefabs[ index_fx ], Vector3(ray_cast_hit.point.x, 0, ray_cast_hit.point.z), Quaternion.identity);	
			if( index_fx >= 8 && index_fx <= 13 ){
				Destroy(GameObject.Find("loop"));
				aux.name = "loop";
			}
		}
	}
	
	//Change-FX keyboard..	
	if ( Input.GetKeyDown("z") || Input.GetKeyDown("left") ){
		Destroy(GameObject.Find("loop"));
		index_fx--;
		if(index_fx <= -1)
			index_fx = fx_prefabs.Length - 1;
		text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;
		if( index_fx >= 8 && index_fx <= 13 ){
			text_fx_name.text = text_fx_name.text + " [LOOP]";
		}
	}
	
	if ( Input.GetKeyDown("x") || Input.GetKeyDown("right")){
		Destroy(GameObject.Find("loop"));
		index_fx++;
		if(index_fx >= fx_prefabs.Length)
			index_fx = 0;
		text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;
		if( index_fx >= 8 && index_fx <= 13 ){
			text_fx_name.text = text_fx_name.text + " [LOOP]";
		}
	}
	
	if ( Input.GetKeyDown("space") ){
		Debug.Break();
		if( index_fx >= 8 && index_fx <= 13 ){
			Destroy(GameObject.Find("loop"));
			aux.name = "loop";
		}
		//Instantiate(fx_prefabs[ index_fx ], Vector3(0, 0, 2), Quaternion.identity);	
	}
	//Hello theere :)	
}