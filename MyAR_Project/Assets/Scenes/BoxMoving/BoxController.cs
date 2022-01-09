using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private const float CameraDistance = 7.5f;
    public float positionY = 0.4f;
    public GameObject[] prefab;

    protected Camera mainCamera;
    protected GameObject HoldingOjbect;
    protected Vector3 InputPosition;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount == 0) return;
#endif
        InputPosition = TouchHelper.TouchPosition;

        if(TouchHelper.Touch2)
        {
            Reset();
            return;
        }
        if(HoldingOjbect)
        {
            if(TouchHelper.IsUp)
            {
                OnPut(InputPosition);
                HoldingOjbect = null;
                return;
            }
            Move(InputPosition);
            return;
        }

        if (!TouchHelper.IsDown) return;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(InputPosition), out var hits,mainCamera.farClipPlane))
        {
            if(hits.transform.gameObject.tag.Equals("Player"))
            {
                HoldingOjbect = hits.transform.gameObject;
                OnHold();
            }
        }
    }
    protected virtual void OnPut(Vector3 pos)
    {
        HoldingOjbect.GetComponent<Rigidbody>().useGravity = true;
        HoldingOjbect.transform.SetParent(null);
    }

    private void Move(Vector3 pos)
    {
        pos.z = mainCamera.nearClipPlane * CameraDistance;
        HoldingOjbect.transform.position = Vector3.Lerp(HoldingOjbect.transform.position, mainCamera.ScreenToWorldPoint(pos), Time.deltaTime * 7f);
    }

    protected virtual void OnHold()
    {
        HoldingOjbect.GetComponent<Rigidbody>().useGravity = false;

        HoldingOjbect.transform.SetParent(mainCamera.transform);
        HoldingOjbect.transform.rotation = Quaternion.identity;
        HoldingOjbect.transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
    }

    private void Reset()
    {
        var pos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
        var index = Random.Range(0, prefab.Length);
        var obj = Instantiate(prefab[index], pos, Quaternion.identity, mainCamera.transform);
        var rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
