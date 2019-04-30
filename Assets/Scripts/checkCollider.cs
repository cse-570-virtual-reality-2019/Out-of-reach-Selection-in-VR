using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollider : MonoBehaviour {

	// Use this for initialization
	public GameObject HaloPrefab;
	public Material newMaterialRef;

	public List<GameObject> intersecting;
	void Start () {
		Debug.Log("Start");
		intersecting=new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
		// Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("torch1") || collision.gameObject.CompareTag("torch2") || collision.gameObject.CompareTag("RightC") || collision.gameObject.CompareTag("LeftC") || collision.gameObject.CompareTag("Room"))
			return;
		Debug.Log(collision.gameObject);
		intersecting.Add(collision.gameObject);
		Debug.Log("Collider enter");
 		// GameObject halo = Instantiate(HaloPrefab) as GameObject;
 		// halo.transform.SetParent(collision.transform, false);
		// Material[] copy=(Material[]) collision.gameObject.GetComponent<Renderer>().materials.Clone();	
		// Material[] n=new Material[copy.Length+1];
		// int i=0;
		// foreach(Material m in copy){
		// 	n[i]=m;
		// 	i+=1;
		// }
		// n[i]=newMaterialRef;
		// collision.gameObject.GetComponent<Renderer>().materials=n;
    }

	private void OnTriggerExit(Collider collision){
		// Debug.Log(collision.gameObject);
		if(collision.gameObject.CompareTag("torch1") || collision.gameObject.CompareTag("torch2") || collision.gameObject.CompareTag("RightC") || collision.gameObject.CompareTag("LeftC") || collision.gameObject.CompareTag("Room")){
			return;
		}
		intersecting.Remove(collision.gameObject);

		Debug.Log("Collider exit");
		//collision.gameObject.GetComponent<Renderer>().material=newMaterialRef;
		// Transform halo = collision.gameObject.transform.Find("Hal(Clone)");
		// Destroy(halo.gameObject);
		// Material[] copy=(Material[]) collision.gameObject.GetComponent<Renderer>().materials.Clone();	
		// Material[] n=new Material[copy.Length-1];
		// Debug.Log(copy[1]);
		// int i=0;
		// for(int j=0;j<copy.Length;j++){
		// 	Debug.Log(copy[j]+" "+i);
		// 	string name=copy[j].ToString();
		// 	if(name.Contains("Emmisive")){
		// 		continue;
		// 	}
		// 	n[i]=copy[j];
		// 	i+=1;
		// }
		// collision.gameObject.GetComponent<Renderer>().materials=n;
	}
	public void Removehalo(){
		// foreach(GameObject g in intersecting){
		// Material[] copy=(Material[]) g.gameObject.GetComponent<Renderer>().materials.Clone();	
		// Material[] n=new Material[copy.Length-1];
		// Debug.Log(copy[1]);
		// int i=0;
		// for(int j=0;j<copy.Length;j++){
		// 	Debug.Log(copy[j]+" "+i);
		// 	string name=copy[j].ToString();
		// 	if(name.Contains("Emmisive")){
		// 		continue;
		// 	}
		// 	n[i]=copy[j];
		// 	i+=1;
		// }
		// g.gameObject.GetComponent<Renderer>().materials=n;
		// }
		// intersecting=new List<GameObject>();
	}
}
