using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogManager : MonoBehaviour
{
    public int val = 1;
    private SpriteRenderer sr;
    private Sprite[] sprite;
    
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (val == 1)
            sprite = gm.sprite;
        if(val == 0)
            sprite = gm.BGsprite;
        sr.sprite = sprite[(int)Random.Range(0, sprite.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
