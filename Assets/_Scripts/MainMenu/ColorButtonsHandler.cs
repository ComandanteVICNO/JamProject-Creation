using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonsHandler : MonoBehaviour
{
    
    [SerializeField]List<ColorButtonData> buttons = new List<ColorButtonData>();
    [SerializeField] private Color chosenColor;
    [SerializeField] private Image playerImage;
    private void Start()
    {
        Debug.Log("Object: " + gameObject.name);
        foreach (ColorButtonData buttonData in buttons)
        {
            buttonData.button.SetupButton(buttonData.color);
        }
    }


    public void ChooseColor(int index)
    {
        chosenColor = buttons[index].color;
        playerImage.color = chosenColor;
        GameSessionManager.instance.SetPlayerColor(chosenColor);
    }
    
    
}
[Serializable]
public class ColorButtonData
{
    public Color color;
    public ColorButton button;
}

