using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public Sprite Sprite
    {
        get => image.sprite;
        set => image.sprite = value;
    }
}
