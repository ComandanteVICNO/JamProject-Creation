using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
   [SerializeField] Transform visualsTransform;
   Vector2 lastDirection = new Vector2(0,1);

   private void Start()
   {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
   }

   private void Update()
   {
      ManangeMovement();
   }

   void ManangeMovement()
   {
      float posX = InputManager.instance.GetPlayerMovement().x;
      float posY = InputManager.instance.GetPlayerMovement().y;
      
      Vector3 direction = new Vector3(posX, posY,0);
      if (direction.sqrMagnitude > 0.0001f)
      {
         direction = direction.normalized;
         transform.position += (direction * UpgradeWallet.instance.GetValue(upgradeType:UpgradeTypes.P_Speed, playerData.speed)) * Time.deltaTime;
         if (direction.x < 0) visualsTransform.localScale = new Vector3(1, 1, 1);
         else if(direction.x > 0) visualsTransform.localScale = new Vector3(-1, 1, 1);
         
         
         lastDirection = new Vector2(direction.x, direction.y);
      }
   }


   public Vector2 GetLastDirection()
   {
      return lastDirection;
   }
}
