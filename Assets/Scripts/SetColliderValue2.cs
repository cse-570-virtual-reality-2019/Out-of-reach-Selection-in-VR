using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderValue2 : MonoBehaviour
{
    public int val;
    public int type;
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
        if(type==2){
          if(this.transform.parent.gameObject.GetComponent<CombineFor2>().grabbed){
            return;
          }
        }
        else{
          if(this.transform.parent.parent.gameObject.GetComponent<CombineFor4>().grabbed){
            return;
          }
        }
        setValue(collision.gameObject);
    }
    
    public void setValue(GameObject other){
        if(type==2){
          GameObject parent=this.transform.parent.gameObject;
          Debug.Log(parent+" "+this.gameObject);
          int type2=getType(other);
          if(type2==2){
              Debug.Log("Simple combine");
              parent.GetComponent<CombineFor2>().simpleCombine(other.transform.parent.gameObject);
          }
          else if(type2==1){
              parent.GetComponent<CombineFor2>().Combine1(other.transform.parent.gameObject,this.transform.position);
          }
        }
        else{
          GameObject parent=this.transform.parent.parent.gameObject;
          Debug.Log(parent+" "+this.gameObject);
          int type2=getType(other);
          if(type2==4){
            Debug.Log("Simple combine");
            parent.GetComponent<CombineFor4>().simpleCombine(other.transform.parent.parent.gameObject);
          }
          else if(type2==1){
            parent.GetComponent<CombineFor4>().Combine1(other.transform.parent.gameObject,this.transform.position);
          }
          else
          {
            bool set=false;
            if(parent.GetComponent<CombineFor4>().first==-1){
              set=true;
              parent.GetComponent<CombineFor4>().first=this.val;
            }
            else if(parent.GetComponent<CombineFor4>().second==-1)
            {
              set=true;
              parent.GetComponent<CombineFor4>().second=this.val;
            }
            print(parent);
            if(set)
              parent.GetComponent<CombineFor4>().Combine2(other.transform.parent.gameObject);
          }
        }
    }

    public int getType(GameObject g){
      if(g.transform.parent.gameObject.CompareTag("One"))
        return 1;
      else if(g.transform.parent.parent!=null && g.transform.parent.parent.gameObject.CompareTag("Four"))
        return 4;
      else
      {
        return 2;
      }
    }
}