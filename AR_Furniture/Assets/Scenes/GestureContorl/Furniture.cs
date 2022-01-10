using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class Furniture : MonoBehaviour
{
    public Camera mainCamera;
    public Transform selectedIcon;
    private LeanDragTranslate _translate;
    private LeanPinchScale _pinch;
    private LeanTwistRotateAxis _axis;

    public bool IsSelected
    {
        get => selectedIcon.gameObject.activeSelf;
        set
        {
            _translate.enabled = _pinch.enabled = _axis.enabled = value;
            selectedIcon.gameObject.SetActive(value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _translate = gameObject.AddComponent<LeanDragTranslate>();
        _pinch = gameObject.AddComponent<LeanPinchScale>();
        _axis = gameObject.AddComponent<LeanTwistRotateAxis>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        selectedIcon.transform.LookAt(mainCamera.transform);
        
    }
}
