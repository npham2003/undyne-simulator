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

    public bool extraArrows = false;
    public bool extraDone = false;

    public bool fireExtra=false;
    public int arrowsAtOnce;

    public float speedMultiplier;

    public int inLevel=1;

    public int beats=0;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        print(gameObject.transform.position);
        allSprites.Add(blueArrows);
        allSprites.Add(greenArrows);
        allSprites.Add(pinkArrows);
        allSprites.Add(violetArrows);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int color;
        int orientation;
        GameObject arrow;
        time+=Time.deltaTime;
        if(inLevel==2){
            
            realTime+=Time.deltaTime;
            if(time>=(60/(float)bpm)){
                time-=60/(float)bpm;
                beat.Play();
                gameObject.GetComponent<BackgroundAnimations>().StartPulse(12/(float)bpm);
                if(realTime<10){
                    color = Random.Range(0,4);
                    orientation = Random.Range(0,4);
                    arrow = Instantiate(arrowPrefab,startingPositions[orientation], Quaternion.identity);
                    if(orientation%2==0){
                        arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.2f,1.65f);
                    }else{
                        arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.65f,1.2f);
                    }
                    arrow.GetComponent<SpriteRenderer>().sprite=allSprites[color][orientation];
                    arrow.GetComponent<ArrowMove>().timeToHit=(60/(float)bpm)*arrowsAtOnce*speedMultiplier;
                    arrow.GetComponent<ArrowMove>().orientation=orientation;
                    arrow.GetComponent<ArrowMove>().color=color;
                    fireExtra=Random.value > 0.5f;
                    extraDone=false;
                }
            }
            if(time>=(30/(float)bpm)&&extraArrows&&!extraDone&&fireExtra&&realTime<10){
                color = Random.Range(0,4);
                orientation = Random.Range(0,4);
                arrow = Instantiate(arrowPrefab,startingPositions[orientation], Quaternion.identity);
                if(orientation%2==0){
                    arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.2f,1.65f);
                }else{
                    arrow.GetComponent<BoxCollider2D>().size=new Vector2(1.65f,1.2f);
                }
                arrow.GetComponent<SpriteRenderer>().sprite=allSprites[color][orientation];
                arrow.GetComponent<ArrowMove>().timeToHit=(60/(float)bpm)*arrowsAtOnce*speedMultiplier;
                arrow.GetComponent<ArrowMove>().orientation=orientation;
                arrow.GetComponent<ArrowMove>().color=color;
                extraDone=true;
            }
            if(realTime>=10 && GameObject.FindGameObjectsWithTag("Arrow").Length==0){
                inLevel=0;
            }
        }else if(inLevel==1){
            
            if(time>=(60/(float)bpm)){
                time-=60/(float)bpm;
                beat.Play();
                gameObject.GetComponent<BackgroundAnimations>().StartPulse(12/(float)bpm);
                beats+=1;
            }
            if(beats==4){
                inLevel=2;
                realTime=0;
            }
        }else{
            print("level end");
            beats=0;
            gameManager.LevelClear();
            if(time>=(60/(float)bpm)){
                time-=60/(float)bpm;
                beat.Play();
                gameObject.GetComponent<BackgroundAnimations>().StartPulse(12/(float)bpm);
            }
        }
    }

    public void ToggleExtraArrows(){
        extraArrows=!extraArrows;
        if(extraArrows){
            gameManager.extraMultiplierOn=1;
        }else{
            gameManager.extraMultiplierOn=0;
        }
        gameManager.LevelUpButtons();
    }
    public void BPMUpButton(){
        bpm+=10;
        gameManager.bpmMultiplierExponent+=1;
        gameManager.LevelUpButtons();
    }
    public void BPMDownButton(){
        Debug.Log("bpm dwn");
        bpm-=10;
        
        gameManager.score-=gameManager.bpmDownCost;
        gameManager.scoreTextGame.text=gameManager.score.ToString();
        gameManager.bpmDownCost*=2;
        gameManager.LevelUpButtons();
    }
}
