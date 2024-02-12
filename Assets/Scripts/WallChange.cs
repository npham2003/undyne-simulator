using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChange : MonoBehaviour
{
    public KeyCode changeSpriteKeyLeft = KeyCode.A; // Change this to the key you want to use
    public KeyCode changeSpriteKeyRight = KeyCode.D;

    public Sprite[] sprites; // Assign your sprites in the Inspector

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int currentSpriteIndex = 0;



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
        if (Input.GetKeyDown(changeSpriteKeyLeft) && sprites.Length > 1)
        {
            ChangeSprite(-1);
            Debug.Log("Changed: ");
        }
        if (Input.GetKeyDown(changeSpriteKeyRight) && sprites.Length > 1)
        {
            ChangeSprite(+1);
            Debug.Log("Changed: ");
        }
    }

    void ChangeSprite(int change)
    {
        currentSpriteIndex = (currentSpriteIndex + change + sprites.Length) % sprites.Length;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ArrowMove moveScript=other.gameObject.GetComponent<ArrowMove>();
        GameManager gameManager=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(!moveScript.failed && moveScript.color==currentSpriteIndex){
            gameManager.score+=(int)(100*(gameManager.extraMultiplier*gameManager.extraMultiplierOn)*(Mathf.Pow(gameManager.bpmUpMultipler,gameManager.bpmMultiplierExponent)));
            gameManager.realScore+=(int)(100*(gameManager.extraMultiplier*gameManager.extraMultiplierOn)*(Mathf.Pow(gameManager.bpmUpMultipler,gameManager.bpmMultiplierExponent)));
            Destroy(other.gameObject);
        }
    }
}