using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public TMP_Text scoreText;

    public TMP_Text levelText;

    public int level=1;
    private ArrowSpawner arrowSpawner;
    [SerializeField]
    private GameObject extraWall;

    public GameObject gameStartPanel;    
    public GameObject gameOverPanel;
    public GameObject levelPanel;

    public int livesCost=100;
    public int bpmDownCost=100;
    public int wallCost=100;

    public UnityEngine.UI.Button livesButton;
    public UnityEngine.UI.Button bpmDownButton;
    public UnityEngine.UI.Button wallButton;
    public UnityEngine.UI.Button extraNotesButton;

    public bool gameOver=false;
    public TMP_Text scoreTextGame;
    public AudioSource boomSound;
    public AudioSource scoreSound;
    public AudioSource gameOverSound;

    public AudioSource nextLevelSound;

    void Start()
    {
        arrowSpawner=this.GetComponent<ArrowSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health==0){
            GameOver();
        }
    }

    public void StartGame(){
        gameStartPanel.SetActive(false);
        arrowSpawner.inLevel=1;
    }

    public void Hurt(){

        health-=1;
        if(health>0){
            boomSound.Play();
        }else{
            gameOverSound.Play();
        }
        if(health>=0){
            healthBar.GetComponent<UnityEngine.UI.Image>().sprite=healthBarSprites[health];
        }
    }
    public void Heal(){
        
        health+=1;
        
        score-=livesCost;
        scoreTextGame.text=score.ToString();
        livesCost*=2;
        LevelUpButtons();
        healthBar.GetComponent<UnityEngine.UI.Image>().sprite=healthBarSprites[health];
    }
    void GameOver(){
        gameOver=true;
        levelText.text="You Made It To Level "+level;
        scoreText.text="Score: "+realScore;
        gameOverPanel.SetActive(true);
    }

    public void ExtraWallButton(){
        score-=wallCost;
        wallCost*=2;
        LevelUpButtons();
        extraWall.SetActive(true);
        extraWall.GetComponent<ExtraWall>().Reset();
    }

    public void Reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelClear(){
        if(!gameOver){
            LevelUpButtons();
            levelPanel.SetActive(true);
        }
    }

    public void ArrowPoints(){
        if(!gameOver){
            scoreSound.Play();
            score+=(int)(100*(Mathf.Pow(extraMultiplier,extraMultiplierOn))*(Mathf.Pow(bpmUpMultipler,bpmMultiplierExponent)));
            realScore+=(int)(100*(Mathf.Pow(extraMultiplier,extraMultiplierOn))*(Mathf.Pow(bpmUpMultipler,bpmMultiplierExponent)));
            scoreTextGame.text=score.ToString();
        }

    }
    public void LevelUpButtons(){
        if(score<livesCost || health==6){
            livesButton.interactable=false;
        }else{
            livesButton.interactable=true;
        }
        if(score<bpmDownCost){
            bpmDownButton.interactable=false;
        }else{
            bpmDownButton.interactable=true;
        }
        if(score<wallCost){
            wallButton.interactable=false;
        }else{
            wallButton.interactable=true;
        }
        if(extraMultiplierOn==1){
            extraNotesButton.interactable=false;
        }else{
            extraNotesButton.interactable=true;
        }
        
        livesButton.GetComponentInChildren<TMP_Text>().text="Recover 1 Health\n"+livesCost+" points";
        bpmDownButton.GetComponentInChildren<TMP_Text>().text="BPM Down\n"+bpmDownCost+" points";
        wallButton.GetComponentInChildren<TMP_Text>().text="Temporary Wall\n"+wallCost+" points";
    }

    public void NewLevel(){
        levelPanel.SetActive(false);
        arrowSpawner.inLevel=1;
        arrowSpawner.bpm+=10;
        nextLevelSound.Play();
        level+=1;
    }
}
