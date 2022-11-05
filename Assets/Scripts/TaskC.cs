using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TaskC : MonoBehaviour
{
    //Get And Change Attached Model
    //Set Threshold
    //Check for Threshold

    [SerializeField] private GameObject hangingActorPrefab;

    private GameObject hangingActorInGame;

    private Rigidbody anchorObjectRigidbody;

    [SerializeField] private float maxSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anchorObjectRigidbody != null) {
            
            if(anchorObjectRigidbody.velocity.magnitude > maxSpeed)
            {
                hangingActorInGame.transform.parent = null;
                hangingActorInGame.transform.DOMoveY(-0.561f, 1.5f);
                hangingActorInGame.transform.DOLocalRotate(new Vector3(-21.51f, 0f, 0f), 1.5f).OnComplete(() => SetRigidBodyNull());
            }
        }
    }

    public void DoTaskC(GameObject OldWalkingActor, GameObject AnchorObject)
    {
        OldWalkingActor.SetActive(false);
        hangingActorInGame = Instantiate(hangingActorPrefab, OldWalkingActor.transform.position, OldWalkingActor.transform.rotation);
        hangingActorInGame.transform.SetParent(AnchorObject.transform);

        anchorObjectRigidbody = AnchorObject.GetComponent<Rigidbody>();
        

    }

    private void SetRigidBodyNull()
    {
        anchorObjectRigidbody = null;
    }
}
