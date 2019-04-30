using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ConeCast : MonoBehaviour
{
    public Transform cameraRigTransform;
    public Transform headTransform; 

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean SelectAction;
    public SteamVR_Action_Boolean SingleSelectAction;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask; 
    public GameObject laserPrefab; 

    private GameObject laser; 
    private Transform laserTransform; 
    private Vector3 hitPoint;
    private List<GameObject> intersectingCopy;
    public SteamVR_Action_Boolean Increase;
    public Material newMaterialRef;

    public Transform camRig;
    private Vector3 camT;
    public Transform Head;

    private bool refinement=false;

    private bool inFirst=false;
    private bool Statedown=false;

   
    void Start()
    {
        laser=laserPrefab;
        laserTransform = laser.transform;
    }

    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
            }
        }
        else 
        {
            if(laser.activeInHierarchy)
            {
                GameObject b1 = GameObject.FindWithTag("torch1");
                b1.GetComponent<checkCollider>().Removehalo();
            }
            laser.SetActive(false);
        }

        if (GetGrip())
        {
            if(laser.activeInHierarchy)
            {
                if(laserTransform.localScale.x<3 && laserTransform.localScale.y<3)
                {
                laserTransform.localScale = new Vector3(laserTransform.localScale.x + 0.01f, laserTransform.localScale.y + 0.01f, laserTransform.localScale.z);
                }
            }
        }

        // if (SelectAction.GetStateUp(handType))
        // {
        //     Debug.Log("Unselected");
        //     Statedown=false;
        //     if(refinement)
        //     {
        //         Debug.Log(camT + " Final " +  camRig.transform.position);
        //         refinement=false;
        //         StartCoroutine(DoLast());
        //         Debug.Log("moving back");
        //     }
        //     else
        //     {
        //         // selected
        //     }
        //     GameObject b1 = GameObject.FindWithTag("torch1"); 
        //     checkCollider c1=b1.GetComponent<checkCollider>();
        //     foreach(GameObject g in c1.intersecting){
        //         Material[] copy=(Material[]) g.gameObject.GetComponent<Renderer>().materials.Clone();	
		//         Material[] n=new Material[copy.Length-1];
        //         Debug.Log(copy[1]);
        //         int i=0;
        //         for(int j=0;j<copy.Length;j++){
        //             Debug.Log(copy[j]+" "+i);
        //             string name=copy[j].ToString();
        //             if(name.Contains("Emmisive")){
        //                 continue;
        //             }
        //             n[i]=copy[j];
        //             i+=1;
        //         }
        //         g.gameObject.GetComponent<Renderer>().materials=n;
        //     }
        // }

        if (SingleSelectAction.GetStateDown(handType)){
             if(laser.activeInHierarchy)
            {
                GameObject b1 = GameObject.FindWithTag("torch1"); 
                checkCollider c1=b1.GetComponent<checkCollider>();
                foreach(GameObject g in c1.intersecting){
                    print(g);
                }
                SelectObjects(c1.intersecting);
                if(refinement){
                    refinement=false;
                    StartCoroutine(DoLast());
                }
            }
        }

        if (SelectAction.GetStateDown(handType))
        {
            Debug.Log("selected");
            Statedown=true;
            if(laser.activeInHierarchy)
            {
                GameObject b1 = GameObject.FindWithTag("torch1"); 
                checkCollider c1=b1.GetComponent<checkCollider>();
                List<GameObject> Selected = new List<GameObject>();
                Debug.Log(c1.intersecting.Count+" Count1");
                if(c1.intersecting.Count>=2)
                {
                    if(!refinement)
                        camT=camRig.transform.position;
                    refinement=true;
                    Debug.Log(camT +" "+camRig.transform.position);
                    Debug.Log("HAll");
                    Vector3 destination=SmallestIndex(c1.intersecting,camRig.position);
                    c1.intersecting=new List<GameObject>();
                    Vector3 difference=camRig.position - Head.position;
                    difference.y=0;
                    Vector3 dest=destination+ difference;
                    dest=Dest(dest,camRig.position);
                    StartCoroutine(MoveToPosition(camRig,dest,1.0f));
                }
                else{
                    SelectObjects(c1.intersecting);
                }
            }
                
        }
    }

    public void SelectObjects(List<GameObject> intersecting)
    {
        foreach(GameObject g in intersecting)
        {
                Material[] copy;
                if(g.transform.parent!=null && (g.transform.parent.gameObject.CompareTag("One") || g.transform.parent.gameObject.CompareTag("Two")))
                {
                    if(g.transform.parent.gameObject.GetComponent<Renderer>().materials.Length>1)
                    {
                        RemoveObjects(g.transform.parent.gameObject);
                        continue;
                    }
                    copy=(Material[])g.transform.parent.gameObject.GetComponent<Renderer>().materials.Clone();
                    Material[] n=new Material[copy.Length+1];
		            int i=0;
		            foreach(Material m in copy)
                    {
                        n[i]=m;
                        i+=1;
                    }
                    n[i]=newMaterialRef;
                    g.transform.parent.gameObject.GetComponent<Renderer>().materials=n;
                    Debug.Log("Done "+n.Length);
                }
                // else if(g.transform.parent!=null && g.transform.parent.parent!= null && (g.transform.parent.gameObject.CompareTag("Four")))
                // {
                //     if(g.transform.parent.parent.gameObject.GetComponent<Renderer>().materials.Length>1)
                //     {
                //         RemoveObjects(g.transform.parent.parent.gameObject);
                //         continue;
                //     }
                //     copy=(Material[])g.transform.parent.parent.gameObject.GetComponent<Renderer>().materials.Clone();
                //     Material[] n=new Material[copy.Length+1];
		        //     int i=0;
		        //     foreach(Material m in copy)
                //     {
                //         n[i]=m;
                //         i+=1;
                //     }
                //     n[i]=newMaterialRef;
                //     g.transform.parent.parent.gameObject.GetComponent<Renderer>().materials=n;
                //     Debug.Log("Done "+n.Length);
                // }
                else
                {
                    if(g.gameObject.GetComponent<Renderer>().materials.Length>1)
                    {
                        RemoveObjects(g);
                        continue;
                    }
                    copy=(Material[]) g.gameObject.GetComponent<Renderer>().materials.Clone();
                    Debug.Log(copy.Length);	
                    Material[] n=new Material[copy.Length+1];
                    int i=0;
                    foreach(Material m in copy)
                    {
                        n[i]=m;
                        i+=1;
                    }
                    n[i]=newMaterialRef;
                    g.gameObject.GetComponent<Renderer>().materials=n;
                    Debug.Log("Done "+n.Length);	
                }
        }
    }
    public void RemoveObjects(GameObject g)
    {
        Debug.Log("Removing");
        Material[] copy=(Material[]) g.gameObject.GetComponent<Renderer>().materials.Clone();	
		Material[] n=new Material[copy.Length-1];
        Debug.Log(copy[1]);
        int i=0;
        for(int j=0;j<copy.Length;j++){
            Debug.Log(copy[j]+" "+i);
            string name=copy[j].ToString();
            if(name.Contains("Emmisive")){
                continue;
            }
            n[i]=copy[j];
                i+=1;
            }
            g.gameObject.GetComponent<Renderer>().materials=n;
    }

    public Vector3 SmallestIndex(List<GameObject> alpha,Vector3 currentPos){
        float bestdis=Mathf.Infinity;
        Vector3 best=currentPos;
        foreach(GameObject g in alpha){
            float dist = Vector3.Distance(g.transform.position,currentPos);
            if(dist<bestdis){
                best=g.transform.position;
                bestdis=dist;
            }
        }
        return best;
    }

    public Vector3 Dest(Vector3 FD,Vector3 SD){
        Vector3 t=FD-SD;
        t=Vector3.Normalize(t);
        Vector3 destination=(FD-(t*2.0f));
        return destination;
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
   {
      var currentPos = transform.position;
      inFirst=true;
      var t = 0f;
      Debug.Log("Run");
       while(t < 1)
       {
            Debug.Log(" Loop Run");
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
      }
      Debug.Log("EndRun");
      inFirst=false;
    }

    public IEnumerator DoLast() {
        while(inFirst)       
            yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveToPosition(camRig,camT,0.1f));
    }

    public bool GetGrip()
    {
        return Increase.GetState(handType);
    }

    public void DecreaseSize(){
         if(laser.activeInHierarchy)
         {
            Debug.Log("Decreasing size");
             if(laserTransform.localScale.x>0.4 && laserTransform.localScale.y>0.4){
                laserTransform.localScale = new Vector3(laserTransform.localScale.x - 0.01f,laserTransform.localScale.y - 0.01f, laserTransform.localScale.z);
             }  
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,laserTransform.localScale.y,hit.distance);
    }

}
