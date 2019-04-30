using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineFor2 : MonoBehaviour {

  private float height;
  public bool grabbed=false;

  void Start () {
    height=1.5f*this.GetComponent<MeshRenderer>().bounds.extents.y;
    print(height+" ..");
  }
  
  // Update is called once per frame
  void Update () {
    
  }
  public void simpleCombine(GameObject g){
      print("Combining with "+this.gameObject);
      g.transform.position=new Vector3(this.transform.position.x,this.transform.position.y+height,this.transform.position.z);
      g.transform.rotation=this.transform.rotation;
  }
  public void Combine1(GameObject g,Vector3 pos){
    g.transform.position=new Vector3(pos.x,pos.y+height,pos.z);
    Debug.Log(g.transform.position);
    g.transform.rotation=this.transform.rotation;
  }
}