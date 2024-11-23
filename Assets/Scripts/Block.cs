using System;
using TMPro;
using UnityEngine;


public class Block : MonoBehaviour
{
    public enum BlockType
    {
        Block,
        PowerBlock
    }
    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;
    private int hitsRemaining = 5;
    public BlockType type;
    private BallLauncher ballLauncher;
    private Color initialColor;
    private readonly Color[] blockColors = new Color[]
    {
        new Color32(146, 1, 203, 255),   // #9201CB (no change)
        new Color32(220, 15, 145, 255),  //  #F715AB 
        new Color32(40, 200, 220, 255),  //  #34EDF3 
        new Color32(200, 20, 20, 255),   //  #EE1717 
        new Color32(230, 90, 30, 255),   //  #FC6524 
        new Color32(230, 200, 40, 255),  //  #FFE433 
        new Color32(0, 220, 80, 255),    //  #00F556 
        new Color32(0, 210, 230, 255),   //  #00F2FA 
        new Color32(90, 30, 230, 255),   //  #6A14FF 
        new Color32(30, 160, 230, 255),  //  #2ABCFF 
        new Color32(220, 30, 200, 255),  //  #FF1ED6 
        new Color32(30, 220, 60, 255),   //  #28FC4B 
        new Color32(220, 220, 20, 255),  //  #FFF019 
        new Color32(110, 190, 220, 255), //  #89D3F0 
        new Color32(220, 100, 150, 255), //  #FD7EAF 
        new Color32(230, 150, 120, 255), //  #FFAA8C 
        new Color32(230, 200, 120, 255), //  #FFDD88 
        new Color32(200, 230, 140, 255), //  #F0FF9D 
        new Color32(130, 0, 220, 255),   //  #9D00FF 
        new Color32(220, 0, 200, 255),   //  #FF00E1 
        new Color32(230, 200, 0, 255),   //  #FFDD00 
        new Color32(0, 160, 220, 255),   //  #00BFFF 
        new Color32(0, 220, 30, 255)     //  #00FF1E 
    };
    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
        if (BlockType.Block == type)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            text = GetComponentInChildren<TextMeshPro>();
            UpdateVisualState();
        }
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        int colorIndex = Mathf.Clamp(hitsRemaining - 1, 0, blockColors.Length - 1); // Ensure index is within bounds
        spriteRenderer.color = blockColors[colorIndex];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == BlockType.Block)
        {
            Debug.Log(collision.gameObject.name);
            hitsRemaining--;
            if (hitsRemaining > 0)
            {
                UpdateVisualState();
            }
            else
            {
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "Touchline")
            {
                Debug.Log("You Touch line :c");
                GameManager.Instance.GameOver();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == BlockType.PowerBlock)
        {
            ballLauncher.CreateBall();
            Destroy(gameObject);
        }
    }
    internal void SetHits(int hits)
    {
        if (BlockType.Block == type)
        {
            hitsRemaining = hits;
            UpdateVisualState();
        }
    }
}


