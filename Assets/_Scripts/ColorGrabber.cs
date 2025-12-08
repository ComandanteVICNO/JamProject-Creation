using System;
using UnityEngine;

public class ColorGrabber : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;

    private void Awake()
    {
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerSprite.color = GameSessionManager.instance.GetPlayerColor();
    }
}
