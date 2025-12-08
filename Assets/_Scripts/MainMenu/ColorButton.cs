using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SetupButton(Color c)
    {
        color = c;
        buttonImage.color = c;
    }
    
}
