using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogWindowFollow : MonoBehaviour
{
    public GameObject player;
    
    void Update()
    {
        transform.localPosition = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y + 1, player.transform.localPosition.z);
    }
}
