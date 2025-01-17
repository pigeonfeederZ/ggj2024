using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Background : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite[] sprites;

    public void SetSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }

    public void ChangeSprite(float breakPossibility)
    {
        if (breakPossibility == 0)
        {
            SetSprite(0);
        }
        else if (breakPossibility < 0.3f)
        {
            SetSprite(1);
        }
        else if (breakPossibility < 0.6f)
        {
            SetSprite(2);
        }
        else
        {
            SetSprite(3);
        }
    }
}
