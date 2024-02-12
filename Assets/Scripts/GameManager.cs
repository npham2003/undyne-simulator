using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int score=0;
    public int realScore=0;
    public float extraMultiplier;
    public int extraMultiplierOn;
    public float bpmUpMultipler;
    public int bpmMultiplierExponent=0;
    public int health=6;
    public GameObject healthBar;
    public Sprite[] healthBarSprites;

    public GameObject gameOverPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health==0){
            GameOver();
        }
    }

    public void Hurt(){
        health-=1;
        if(health>=0){
            healthBar.GetComponent<UnityEngine.UI.Image>().sprite=healthBarSprites[health];
        }
    }
    public void Heal(){
        health+=1;
        healthBar.GetComponent<UnityEngine.UI.Image>().sprite=healthBarSprites[health];
    }
    void GameOver(){
        gameOverPanel.SetActive(true);
    }
}
