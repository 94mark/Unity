using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] bodyObjects;
    public Color32[] colors;
    public float rotSpeed = 0.1f;

    Material[] carMats;

    // Start is called before the first frame update
    void Start()
    {
        //carMats �迭�� �ڵ��� �ٵ� ������Ʈ�� ����ŭ �ʱ�ȭ�Ѵ�
        carMats = new Material[bodyObjects.Length];

        //�ڵ��� �ٵ� ������Ʈ�� ���͸��� ������ carMats �迭�� �����Ѵ�
        for(int i = 0; i < carMats.Length; i++)
        {
            carMats[i] = bodyObjects[i].GetComponent<MeshRenderer>().material;
        }
        //���� �迭 0������ ���͸����� �ʱ� ������ �����Ѵ�
        colors[0] = carMats[0].color;
    }

    public void ChangeColor(int num)
    {
        //�� LOD ���͸����� ������ ��ư�� ������ �������� �����Ѵ�
        for (int i = 0; i < carMats.Length; i++)
        {
            carMats[i].color = colors[num];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��ġ�� ������ 1�� �̻��̶��
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //����, ��ġ ���°� �����̰� �ִ� ���̶��
            if(touch.phase == TouchPhase.Moved)
            {
                //����, ī�޶� ��ġ���� ���� �������� ���̸� �߻��� �ε��� �����
                //8�� ���̾��� ��ġ �̵����� ���Ѵ�
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1<<8))
                {
                    Vector3 deltaPos = touch.deltaPosition;

                    //���� �����ӿ��� ���� �����ӱ����� X�� ��ġ �̵����� �����
                    //���� Y�� �������� ȸ����Ų��
                    transform.Rotate(transform.up, deltaPos.x * -1.0f * rotSpeed);
                }
            }
        }
    }
}