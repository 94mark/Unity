using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceController : MonoBehaviour
{
    public Camera mainCamera;
    public ARRaycastManager raycastMananger;
    public GameObject PlacementIndicator;
    public GameObject[] prefab;
    private Dictionary<int, GameObject> _instancePrefab = new Dictionary<int, GameObject>();

    // Update is called once per frame
    void Update()
    {
        var hits = new List<ARRaycastHit>();
        var center = mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        raycastMananger.Raycast(center, hits, TrackableType.PlaneWithinBounds);
        PlacementIndicator.SetActive(hits.Count > 0);
        if (hits.Count == 0) return;

        var cameraForward = mainCamera.transform.TransformDirection(Vector3.forward);
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        var pose = hits[0].pose;
        pose.rotation = Quaternion.LookRotation(cameraBearing);
        PlacementIndicator.transform.SetPositionAndRotation(pose.position,pose.rotation);

        if(TouchHelper.Touch3)
        {
            var index = Random.Range(0, prefab.Length);
            var obj = Instantiate(prefab[index], pose.position, pose.rotation, transform);
            obj.SetActive(true);
            _instancePrefab[obj.GetInstanceID()] = obj;
            RefreshSelection(obj);
        }

        if (Input.touchCount == 0) return;

        if(TouchHelper.IsDown)
        {
            if(Physics.Raycast(mainCamera.ScreenPointToRay(TouchHelper.TouchPosition), out var raycastHits, mainCamera.farClipPlane))
            {
                if(raycastHits.transform.gameObject.tag.Equals("Player"))
                {
                    RefreshSelection(raycastHits.transform.gameObject);
                }
            }
        }
    }
    private void RefreshSelection(GameObject select)
    {
        foreach(var obj in _instancePrefab)
        {
            var furniture = obj.Value.GetComponent<Furniture>();
            if(furniture)
            {
                furniture.IsSelected = obj.Key == select.GetInstanceID();
            }
        }
    }
}
