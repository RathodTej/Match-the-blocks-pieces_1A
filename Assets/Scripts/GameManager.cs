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
    public GameObject Audios;
    public GameObject Selector;
    public Sprite[] sprite;
    public Sprite[] BGsprite;

    public Transform[] Uis;


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
        Board.gameObject.SetActive(false);
        StaticManager.isGamePause = true;
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(1f);
        seq.AppendCallback(CardpopUp);
        seq.AppendInterval(5f);
        seq.AppendCallback(CardRotate);
        seq.AppendInterval(1.5f);
        seq.AppendCallback(RandomCardOpen);
        seq.AppendInterval(1f);
        seq.AppendCallback(LetsGoPopUp);
        seq.AppendInterval(.5f);
        seq.AppendCallback(UiPopUps);
        seq.AppendInterval(1f);
        seq.AppendCallback(StartGame);

        winPanal.gameObject.SetActive(false);
        StaticManager.isGameOver = false;
        


        Uis[0].localScale = Vector3.zero;
        Debug.Log(StaticManager.levelName);
    }

    // Update is called once per frame
    void Update()
    {
        if(openCardsCount == cardsCount && !StaticManager.isGameOver)
        {
            StaticManager.isGamePause = true;
            Debug.Log("GameOver");
            StartCoroutine(GameOverDelay());
        }
    }


    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2);
        Audios.transform.GetChild(0).gameObject.SetActive(true);
        StaticManager.isGameOver = true;
        winPanal.gameObject.SetActive(true);
    }

    public void CardpopUp()
    {
        /* Sequence seq = DOTween.Sequence();

         foreach(Transform card in Board)
         {
             seq.Append( card.DOScale(new Vector3(.7f, .7f, .7f), doTDuration*0.7f));
         }*/
        Board.gameObject.SetActive(true);
        Board.DOScale(new Vector3(0,0,0), doTDuration).From();
    }

    public void CardRotate()
    {
        Sequence seq = DOTween.Sequence();

        foreach(Transform card in Board)
        {

            /*seq.Append(card.DORotate(new Vector3(0, 180, 0), doTDuration*.5f))
                .AppendInterval(doTDelayTime*1.3f)
                .Append(card.DORotate(new Vector3(0, 0, 0), doTDuration * .5f));
        */
           card.DORotate(new Vector3(0, 0, 0), doTDuration);

        }
       // Debug.Log("Rotated");
    }    

    public void LetsGoPopUp()
    {
        letsGo.DOScale(new Vector3(2.5f,2.5f,2.5f), doTDuration).SetEase(Ease.InOutFlash)
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
                //Debug.Log($"{i} plased");
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
        Selector.SetActive(true);
        Selector.transform.SetParent(card);
        Selector.transform.localPosition = new Vector3(0, 0, 0.1f);
        
    }

    public void UiPopUps()
    {
        
        Uis[0].DOScale(new Vector3(1, 1, 1), doTDuration*2).SetEase(Ease.OutElastic);
    }

    public void StartGame()
    {
        StaticManager.isGamePause = false;
    }

    public void GamePause(bool pause)
    {
        StaticManager.isGamePause = pause;
    }
    

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }



}
