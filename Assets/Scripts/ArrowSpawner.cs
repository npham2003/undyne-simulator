using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public int bpm;
    public float time;
    public AudioSource beat;
    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if(time>=(60/(float)bpm)){
            time-=60/(float)bpm;
            beat.Play();
        }
    }
}
