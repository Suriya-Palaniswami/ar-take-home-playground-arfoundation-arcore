using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class TaskC : MonoBehaviour
{
    //Get And Change Attached Model
    //Set Threshold
    //Check for Threshold

    [SerializeField] private GameObject hangingActorPrefab;

    private GameObject hangingActorInGame;

    private Rigidbody anchorObjectRigidbody;

    [SerializeField] private float maxSpeed;

    [SerializeField] private TextMeshProUGUI hintText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (anchorObjectRigidbody != null) {

            hintText.text = "Not Null";
            if(anchorObjectRigidbody.velocity.magnitude > maxSpeed)
            {
                hangingActorInGame.transform.parent = null;
                hangingActorInGame.transform.DOMoveY(-0.561f, 1.5f);
                hangingActorInGame.transform.DOLocalRotate(new Vector3(-21.51f, 0f, 0f), 1.5f).OnComplete(() => SetRigidBodyNull());
                hintText.text = anchorObjectRigidbody.velocity.magnitude.ToString();
            }
        }
    }

    public void DoTaskC(GameObject OldWalkingActor, GameObject AnchorObject)
    {
        OldWalkingActor.SetActive(false);
        hangingActorInGame = Instantiate(hangingActorPrefab, OldWalkingActor.transform.position, OldWalkingActor.transform.rotation);
        hangingActorInGame.transform.SetParent(AnchorObject.transform);
        anchorObjectRigidbody = hangingActorInGame.GetComponent<Rigidbody>();
        hintText.text = anchorObjectRigidbody.ToString();

    }

    private void SetRigidBodyNull()
    {
        anchorObjectRigidbody = null;
    }
}
