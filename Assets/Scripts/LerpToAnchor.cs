using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToAnchor : MonoBehaviour
{

    public GameObject from;
    public GameObject to;
    public bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(check) transform.position = Vector3.MoveTowards(transform.position, to.transform.position, 3.5f*Time.deltaTime);
    }
    
}
