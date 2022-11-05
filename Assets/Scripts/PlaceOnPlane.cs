using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    UnityEvent placementUpdate;

    [SerializeField]
    GameObject visualObject;

    private List<GameObject> placedList = new List<GameObject>();

    [SerializeField]
    private int maxPrefabSpwanCount = 0;
    private int placedPrefabCount;


    [SerializeField] public bool GoTo = false;

    [SerializeField] public GameObject GoToButton;
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();

        if (placementUpdate == null)
            placementUpdate = new UnityEvent();

        placementUpdate.AddListener(DiableVisual);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            if (placedPrefabCount < maxPrefabSpwanCount)
            {
                SpawnPrefab(hitPose);
            }
            placementUpdate.Invoke();
        }

        if (GoTo) transform.position = Vector3.MoveTowards(placedList[0].transform.position, placedList[1].transform.position, 3.5f * Time.deltaTime);

        if (placedList.Count >= 2) GoToButton.SetActive(true);
    }

    public void SetPrefab(GameObject prefabType)
    {
        m_PlacedPrefab = prefabType;
    }
    private void SpawnPrefab(Pose hitPose)
    {
        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
        placedList.Add(spawnedObject);
        placedPrefabCount++;
    }

    public void LerpTo()
    {
        if (GoTo == false) GoTo = true;
    }
    public void DiableVisual()
    {
        visualObject.SetActive(false);
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}