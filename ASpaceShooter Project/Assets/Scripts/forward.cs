using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class forward : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Invoke(nameof(cameraAttach), 5f);
        Invoke(nameof(Rotate), 8f);
        _rigidbody.velocity = transform.forward*13;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        cameraAttach();
        cameraMove();
    }

    void cameraMove()
    {
        
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView -= 0.01f;
    }
    void cameraAttach()
    {
        //if (transform.position.x > -177.7 || transform.position.y > 116.4 || transform.position.z < -4.5)
        GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
    }
    

    void Rotate()
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }
    
    
    
}


