using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public int bpm;
    public float time;
    public float realTime;
    public AudioSource beat;
    public GameObject arrowPrefab;

    public Vector3[] startingPositions = {new Vector3(6,0,0),new Vector3(0,6,0), new Vector3(-6,0,0), new Vector3(0,-6,0)};

    public List<Sprite> blueArrows;
    public List<Sprite> greenArrows;
    public List<Sprite> pinkArrows;
    public List<Sprite> violetArrows;
    public List<List<Sprite>> allSprites = new();

    public int arrowsAtOnce;

    public float speedMultiplier;

    public int inLevel=1;

    public int beats=0;
    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.transform.position);
        allSprites.Add(blueArrows);
        allSprites.Add(greenArrows);
        allSprites.Add(pinkArrows);
        allSprites.Add(violetArrows);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inLevel==2){
            time+=Time.deltaTime;
            realTime+=Time.deltaTime;
            if(time>=(60/(float)bpm)){
                time-=60/(float)bpm;
                beat.Play();
                if(realTime<10){
                    int color = Random.Range(0,4);
                    int orientation = Random.Range(0,4);
                    GameObject arrow = Instantiate(arrowPrefab,startingPositions[orientation], Quaternion.identity);
                    if(orientation%2==0){
                        arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.2f,1.65f);
                    }else{
                        arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.65f,1.2f);
                    }
                    arrow.GetComponent<SpriteRenderer>().sprite=allSprites[color][orientation];
                    arrow.GetComponent<ArrowMove>().timeToHit=(60/(float)bpm)*arrowsAtOnce*speedMultiplier;
                    arrow.GetComponent<ArrowMove>().orientation=orientation;
                    arrow.GetComponent<ArrowMove>().color=color;
                }
            }
            if(realTime>=10 && GameObject.FindGameObjectsWithTag("Arrow").Length==0){
                inLevel=0;
            }
        }else if(inLevel==1){
            time+=Time.deltaTime;
            if(time>=(60/(float)bpm)){
                time-=60/(float)bpm;
                beat.Play();
                beats+=1;
            }
            if(beats==4){
                inLevel=2;
                realTime=0;
            }
        }else{
            print("level end");
        }
    }
}
