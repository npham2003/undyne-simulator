using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraWall : MonoBehaviour
{
    public int blockCount = 2; // count of arrows it can block
    public GameObject wallPivot;

    private List<float> angles = new List<float> { 0f, 90f, 180f, 270f };
    public int randomIdx = 0;
    public bool wallEnabled = true;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // randomly decides the position 
        randomIdx = Random.Range(0, angles.Count);
        wallPivot.transform.rotation = Quaternion.Euler(0f, 0f, angles[randomIdx]);
    }

    // Update is called once per frame
    void Update()
    {
        if(blockCount == 0)
        {
            gameObject.SetActive(false);

        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ArrowMove moveScript=other.gameObject.GetComponent<ArrowMove>();
        GameManager gameManager=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(!moveScript.failed && blockCount > 0){
            gameManager.ArrowPoints();
            Destroy(other.gameObject);

            blockCount -= 1; // decrease the available blocking count
        }
    }

}
