using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColor : MonoBehaviour
{
    private SpriteRenderer carBody;
    private SpriteRenderer carRoof;

    private List<SpriteRenderer> carSprites = new List<SpriteRenderer>();

    public void SetCarPartsColor(List<Color32> carColors, int colorStart)
    {
        int j = 0;
        for (int i = colorStart; i < carColors.Count; i++, j++)
        {
            if (j >= carSprites.Count)
            {
                return;
            }

            carSprites[j].color = carColors[i];
        }
    }

    public void InitializeCarParts(int playerIndex)
    {
        carBody = gameObject.transform.GetChild(playerIndex).Find("Body").GetComponent<SpriteRenderer>();
        carRoof = gameObject.transform.GetChild(playerIndex).Find("Roof").GetComponent<SpriteRenderer>();

        carSprites.Add(carBody);
        carSprites.Add(carRoof);
    }
}
