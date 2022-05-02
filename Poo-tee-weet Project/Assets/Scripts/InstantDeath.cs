using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
   public PlayerMovement player;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.tag == "Player")
      {
         player.ChangeHealth(-50);
         print(player);
         print("collided");
      }
   }
}
