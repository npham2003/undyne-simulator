using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public int bpm;
    public float timeToHit;
    public int orientation;

    public float distance;

    public float firstAccept;
    public float lastAccept;
    public float movePerFrame;

    public int color;

    public bool failed = false;

    // Start is called before the first frame update
    void Start()
    {
        distance=6-firstAccept;
        movePerFrame = distance/(timeToHit*60);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeToHit!=0){
            movePerFrame = distance/(timeToHit*50);
            if(orientation==0){
                gameObject.transform.position=new Vector3(gameObject.transform.position.x-movePerFrame,gameObject.transform.position.y,gameObject.transform.position.z);
                if(gameObject.transform.position.x<=lastAccept){
                    failed = true;
                }
            }
            if(orientation==1){
                gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-movePerFrame,gameObject.transform.position.z);
                if(gameObject.transform.position.y<=lastAccept){
                    failed = true;
                }
            }
            if(orientation==2){
                gameObject.transform.position=new Vector3(gameObject.transform.position.x+movePerFrame,gameObject.transform.position.y,gameObject.transform.position.z);
                if(gameObject.transform.position.x>=-lastAccept){
                    failed = true;
                }
            }
            if(orientation==3){
                gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+movePerFrame,gameObject.transform.position.z);
                if(gameObject.transform.position.y>=-lastAccept){
                    failed = true;
                }
            }

        }
        if(Vector3.Distance(new Vector3(0,0,0),gameObject.transform.position)<=0.1){
            Destroy(gameObject);
        }
        
    }
}
