using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChange : MonoBehaviour
{
    public KeyCode changeSpriteKey = KeyCode.Space; // Change this to the key you want to use
    public Sprite[] sprites; // Assign your sprites in the Inspector

    private SpriteRenderer spriteRenderer;
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
        if (Input.GetKeyDown(changeSpriteKey) && sprites.Length > 1)
        {
            ChangeSprite();
            Debug.Log("Changed: ");
        }
    }

    void ChangeSprite()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }
}