using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform Cards;
    public Transform Board;
    public Transform letsGo;
    public Transform winPanal;
    public Sprite[] sprite;
    public Sprite[] BGsprite;
    public float doTDuration;
    public float doTDelayTime;
    public string cardName;

    public int cardsCount = 0;
    public int openCardsCount = 0;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        cardsCount = Cards.childCount;
        openCardsCount = 0;
        CardSort();

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(1.5f);
        seq.AppendCallback(CardpopUp);
        seq.AppendInterval(.5f);
        seq.AppendCallback(CardRotate);
        seq.AppendInterval(13);
        seq.AppendCallback(LetsGoPopUp);
        seq.AppendInterval(2);
        seq.AppendCallback(RandomCardOpen);

        winPanal.gameObject.SetActive(false);
        StaticManager.isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(openCardsCount == cardsCount && !StaticManager.isGameOver)
        {
            Debug.Log("GameOver");
            StaticManager.isGameOver = true;
            winPanal.gameObject.SetActive(true);
        }
    }

    public void CardpopUp()
    {
        Sequence seq = DOTween.Sequence();

        foreach(Transform card in Board)
        {
            seq.Append( card.DOScale(new Vector3(.7f, .7f, .7f), doTDuration*.2f));
        }
    }

    public void CardRotate()
    {
        Sequence seq = DOTween.Sequence();

        foreach(Transform card in Board)
        {
            seq.Append(card.DORotate(new Vector3(0, 180, 0), doTDuration*.5f))
                .AppendInterval(doTDelayTime*1.3f)
                .Append(card.DORotate(new Vector3(0, 0, 0), doTDuration * .5f));
        }
       // Debug.Log("Rotated");
    }    

    public void LetsGoPopUp()
    {
        letsGo.DOScale(new Vector3(3,3,3), doTDuration * 5).SetEase(Ease.InOutFlash)
            .OnComplete(()=>
            {
                letsGo.DOScale(new Vector3(0, 0, 0), doTDuration).SetEase(Ease.InOutFlash);
            });
    }

    public void CardSort()
    {
        float x= -5.24f;
        float y = 5.3f;
        float addval = 2.11f;

        while (Cards.childCount>0)
        {
            int cardsCount = Cards.childCount;
            GameObject card = Cards.GetChild(Random.Range(0, cardsCount)).gameObject;

            card.transform.SetParent(Board.transform);
        }

        for(int i=0;i<Board.childCount;i++)
        {
            GameObject card = Board.GetChild(i).gameObject;
            if (i%6==0)
            {
                y = y - addval;
                x = -5.24f;
                card.transform.position = new Vector3(x,y,0);
            }
            else
            {
                x = x + addval;
                card.transform.position = new Vector3(x, y , 0);
            }
        }
    }

    public void RandomCardOpen()
    {
        Transform card = Board.GetChild((int)Random.Range(0, Board.childCount));

        card.DORotate(new Vector3(0, 180, 0), doTDuration);
        card.GetComponent<CardLogoHolder>().isOpen = true;
        StaticManager.toOpen = false;
        cardName = card.name;
        //Debug.Log("open");
        openCardsCount += 1;
    }


    

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }



}
