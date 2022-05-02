using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone5 : MonoBehaviour
{
   private void OnTriggerStay2D(Collider2D other)
   {
      controller5 controller = other.GetComponent<controller5>();
      if (controller != null)
      {
         controller.ChangeHealth(-1);
      }
   }
}
