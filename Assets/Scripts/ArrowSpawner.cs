using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public int bpm;
    public float time;
    public AudioSource beat;
    public GameObject arrowPrefab;

    public Vector3[] startingPositions = {new Vector3(6,0,0),new Vector3(0,6,0), new Vector3(-6,0,0), new Vector3(0,-6,0)};

    public List<Sprite> blueArrows;
    public List<Sprite> greenArrows;
    public List<Sprite> pinkArrows;
    public List<Sprite> violetArrows;
    public List<List<Sprite>> allSprites = new List<List<Sprite>>();

    public int arrowsAtOnce;

    public float speedMultiplier;
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
        time+=Time.deltaTime;
        if(time>=(60/(float)bpm)){
            time-=60/(float)bpm;
            beat.Play();
            
            int color = Random.Range(0,4);
            int orientation = Random.Range(0,4);
            GameObject arrow = Instantiate(arrowPrefab,startingPositions[orientation], Quaternion.identity);
            arrow.GetComponent<SpriteRenderer>().sprite=allSprites[color][orientation];
            arrow.GetComponent<ArrowMove>().timeToHit=(60/(float)bpm)*arrowsAtOnce*speedMultiplier;
            arrow.GetComponent<ArrowMove>().orientation=orientation;
        }
    }
}
