using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] backgroundCircles;
    public float disappearRate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<backgroundCircles.Length;i++){
            print(backgroundCircles[i].GetComponent<SpriteRenderer>().color.a);
            Color color = backgroundCircles[i].GetComponent<SpriteRenderer>().color;
            color.a=color.a-=Time.deltaTime*disappearRate;
            backgroundCircles[i].GetComponent<SpriteRenderer>().color=color;
        }
        print(backgroundCircles[0].GetComponent<SpriteRenderer>().color);
    }

    
    public void StartPulse(float time){
        StartCoroutine(Pulse(time));
    }
    public IEnumerator Pulse(float time){
        for(int i=0;i<backgroundCircles.Length;i++){
            backgroundCircles[i].GetComponent<SpriteRenderer>().color=new Color(backgroundCircles[i].GetComponent<SpriteRenderer>().color.r,backgroundCircles[i].GetComponent<SpriteRenderer>().color.g,backgroundCircles[i].GetComponent<SpriteRenderer>().color.b,1);
            yield return new WaitForSeconds(time);
        }
    }
}
