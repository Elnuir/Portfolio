using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    Animation anim;


    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    public void GoToSettings()
    {
        anim.Play("GoToSettings");
    }
    public void BackFromSettings()
    {
        anim.Play("BackFromSettings");
    }
    public void GoToLevels()
    {
        anim.Play("GoToLevels");
    }
    public void BackFromLevels()
    {
        anim.Play("BackFromLevels");
    }
    public void Kitti()
    {
        anim.Play("Kitti");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(0,0,0);
        }
    }

}
