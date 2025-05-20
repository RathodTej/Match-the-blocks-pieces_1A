using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardLogoHolder : MonoBehaviour
{
    public bool isOpen = false;
    //public bool toOpen = false;

    GameManager gm;
    float duration;
    float delaTime;
    string cardName;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        duration = gm.doTDuration;
        delaTime = gm.doTDelayTime;
        cardName = gameObject.name;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Debug.Log(gameObject.name);
       /* Debug.Log($"{name} should open {StaticManager.toOpen} ,.. " +
            $"card is open {isOpen} ,.. {gm.cardName} is Tobe open");*/

        if(StaticManager.toOpen)
        {
            if (!isOpen){
                StaticManager.toOpen = false;
                OpenCard();
                isOpen = true;
            }
        }
        else
        {
            if (cardName == gm.cardName && !isOpen)
            {
                StaticManager.toOpen = true;
                OpenCard();
                isOpen = true;
            }
            if (cardName != gm.cardName && !isOpen)
            {
                ShowCard();
            }
        }
        
        
    }


    public void OpenCard()
    {
        transform.DORotate(new Vector3(0, 180, 0), duration);
        if(!StaticManager.toOpen)
        {
            gm.cardName = transform.gameObject.name; 
        }
        gm.openCardsCount += 1;
    }

    public void ShowCard()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DORotate(new Vector3(0, 180, 0), duration))
            .AppendInterval(delaTime)
            .Append(transform.DORotate(new Vector3(0, 0, 0), duration))
            .AppendInterval(delaTime)
            .SetLoops(1);
    }


}
