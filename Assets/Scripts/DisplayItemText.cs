using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayItemText : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI currentItem;

    public void InitializeItemText(GameManager _gameManager, int playerIndex)
    {
        gameManager = _gameManager;
        currentItem = Instantiate(currentItem, gameManager.canvas.transform);
        currentItem.transform.position = gameManager.itemPositions[playerIndex].position;
    }

    public void SetText(string text)
    {
        currentItem.text = text;
    }
}
