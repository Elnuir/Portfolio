using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoarder : MonoBehaviour
{
    [SerializeField] private GameObject dangerMessage;

    [SerializeField] private AudioManager _audioManager;
    

    private void OnTriggerExit(Collider other)
    {
        PlayerControl1 player = other.GetComponent<PlayerControl1>();
        if (player != null)
        {
            player.isRadiated = true;
            player.OutOfWorldBoarders();
            dangerMessage.SetActive(true);
            _audioManager.Play("GeyGerSound");
            _audioManager.Play("WarningSound");
        }
        if (other.gameObject.tag == "Enemy")
        {
            var a = other.gameObject.GetComponent<Enemy1>();
            a.EnemyDeath();

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerControl1 player = other.GetComponent<PlayerControl1>();
        if (player != null)
        {
            player.isRadiated = false;
            dangerMessage.SetActive(false);
            _audioManager.Stop("GeyGerSound");
            _audioManager.Stop("WarningSound");
        }
    }
}
