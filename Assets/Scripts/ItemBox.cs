using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    GameManager gameManager;
    
    [HideInInspector]
    public List<GameObject> items = new List<GameObject>();

    private int random; 

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        items.Clear();
        
        for(int i = 0; i < 4; i++)
        {
            items.Add(gameObject.transform.GetChild(i).gameObject);
        }
        
        RandomizeItemBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.ItemBox(this);
        gameObject.SetActive(false);

    }

    public int GetRandomInt()
    {
        random = Random.Range(0, items.Count);
        return random;
    }

    public void RandomizeItemBox()
    {
        items[GetRandomInt()].SetActive(true);
    }

}
