using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineFor4 : MonoBehaviour {

  public float height;
  private float width;
  public int first=-1;
  public int second=-1;
  public bool grabbed=false;

  void Start () {
    height=1.5f*this.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.extents.y;
    width=this.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.extents.x;
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

  private void simpleCombine2(GameObject g1,GameObject g2){
      // g2.transform.position=new Vector3(g1.transform.position.x,g1.transform.position.y+height,g1.transform.position.z);
      float x=g1.transform.position.x+g2.transform.position.x;
      x=x/2;
      g2.transform.position=new Vector3(x,g1.transform.position.y+height,g1.transform.position.z);
      g2.transform.rotation=g1.transform.rotation;
  }

  public void Combine2(GameObject g){
    if(first==-1 || second==-1){
      return;
    }
    if(first+second==3){
      Debug.Log("Combine for 3");
      GameObject child1=this.transform.GetChild(0).gameObject;
      simpleCombine2(child1,g); 
      reset();
    }
    else if(first+second==7){
      Debug.Log("Combine for 7");
      GameObject child2=this.transform.GetChild(1).gameObject;
      simpleCombine2(child2,g);
      reset();
    }
    else if(first+second==4){
      GameObject child1=this.transform.GetChild(0).gameObject;
      simpleCombine2(child1,g); 
      print("rotating");
      print(g.transform.rotation);
     // g.transform.Rotate(0,90,0);
      print(g.transform.rotation);
      reset();
    }
    else if(first+second==6){
      GameObject child1=this.transform.GetChild(0).gameObject;
      simpleCombine2(child1,g); 
      reset();
    }
    else{
      reset();
    }

  }
  public void reset(){
    first=-1;
    second=-1;
  }

}