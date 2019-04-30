using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderValue1 : MonoBehaviour
{
    public int val;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision){
        Debug.Log("Collision");
        if(collision.gameObject.CompareTag("torch1") || collision.gameObject.CompareTag("torch2"))
          return;
        if(this.transform.parent.gameObject.GetComponent<CombineFor1>().grabbed)
          return;
        setValue(collision.gameObject);
    }
    
    public void setValue(GameObject other){
        GameObject parent=this.transform.parent.gameObject;
        Debug.Log(parent+" "+this.gameObject);
        int type2=getType(other);
        if(type2==1){
            Debug.Log("Simple combine");
            parent.GetComponent<CombineFor1>().simpleCombine(other.transform.parent.gameObject);
        }
    }

  public int getType(GameObject g){
    if(g.transform.parent.gameObject.CompareTag("One"))
      return 1;
    else if(g.transform.parent.parent.gameObject!=null && g.transform.parent.parent.gameObject.CompareTag("Four"))
      return 4;
    else
    {
      return 2;
    }
  }
}