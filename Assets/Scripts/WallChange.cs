using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChange : MonoBehaviour
{
    public KeyCode changeSpriteKeyLeft = KeyCode.A; // Change this to the key you want to use
    public KeyCode changeSpriteKeyRight = KeyCode.D;
    public KeyCode changeSpriteKeyUp = KeyCode.W;
    public KeyCode changeSpriteKeyDown = KeyCode.S;

    public Sprite[] sprites; // Assign your sprites in the Inspector

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int currentSpriteIndex = 0;

    public Color[] colors;

    public GameObject[] backgroundCircles;
    public List<float> angles = new List<float>{90f, 0f, -90f, 180f};

    public GameObject wheel;
   
    public AudioSource changeSound;

    public GameObject gameManager;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
       
        
    }

    void Update()
    {
        if(!gameManager.GetComponent<GameManager>().gameOver&&gameManager.GetComponent<ArrowSpawner>().inLevel!=3){
            if (Input.GetKeyDown(changeSpriteKeyLeft) && sprites.Length > 1)
            {
                ChangeSprite(1);
                Debug.Log("Changed: ");
            }
            if (Input.GetKeyDown(changeSpriteKeyRight) && sprites.Length > 1)
            {
                ChangeSprite(3);
                Debug.Log("Changed: ");
            }
            if (Input.GetKeyDown(changeSpriteKeyUp) && sprites.Length > 1)
            {
                ChangeSprite(0);
                Debug.Log("Changed: ");
            }
            if (Input.GetKeyDown(changeSpriteKeyDown) && sprites.Length > 1)
            {
                ChangeSprite(2);
                Debug.Log("Changed: ");
            }
        }
    }

    void ChangeSprite(int change)
    {
        currentSpriteIndex = change;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        
        Debug.Log("Current index: " + currentSpriteIndex);
        changeSound.Play();
        for(int i=0;i<backgroundCircles.Length;i++){
            backgroundCircles[i].GetComponent<SpriteRenderer>().color = new Color(colors[currentSpriteIndex].r,colors[currentSpriteIndex].g,colors[currentSpriteIndex].b,backgroundCircles[i].GetComponent<SpriteRenderer>().color.a);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ArrowMove moveScript=other.gameObject.GetComponent<ArrowMove>();
        GameManager gameManager=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(!moveScript.failed && moveScript.color==currentSpriteIndex){
            gameManager.ArrowPoints();
            Destroy(other.gameObject);
        }
    }
}