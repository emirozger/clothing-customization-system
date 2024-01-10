using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public ItemData[] datas;
    public Transform cardsPanel;
    public Vector2 outerWidthHeight;
    public Vector2 innerWidthHeight;
    public Sprite itemOuterSprite;

    private void Awake()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SpawnItemPreviews();
    }

    private void SpawnItemPreviews()
    {
        foreach (ItemData item in datas)
        {
            if (item.image != null)
            {
                GameObject card = new GameObject("Card");
                card.transform.parent = cardsPanel.transform;
                var cardImage = card.AddComponent<Image>();
                var tempColor = cardImage.color;
                tempColor.a = 0f;
                cardImage.color = tempColor;
                
              
                
                GameObject outer = new GameObject("outer");
                outer.transform.parent = card.transform;
                RectTransform outerRectTransform = outer.AddComponent<RectTransform>();
                outerRectTransform.sizeDelta = outerWidthHeight; 
                var outerImage = outer.AddComponent<Image>();
                outerImage.sprite = itemOuterSprite;
                outerImage.color = Color.green;
                //temp
                //outerImage.gameObject.SetActive(false);

                GameObject inner = new GameObject("inner");
                inner.transform.parent = card.transform;
                RectTransform innerRectTransform = inner.AddComponent<RectTransform>(); 
                innerRectTransform.sizeDelta = innerWidthHeight; 

                var script = inner.AddComponent<DragUIItem>();
                script.instanceMaterial = item.material;

                Image imageComponent = inner.AddComponent<Image>();
                imageComponent.sprite = item.image;
            }
        }
    }

}