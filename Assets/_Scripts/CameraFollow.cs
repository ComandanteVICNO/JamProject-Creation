using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float zOffset = -10;
    GameObject player;
    void Start()
    {
      player = GameObject.Find("Player");
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zOffset);
    }
}
