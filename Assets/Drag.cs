using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshCollider))]

public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;


    void OnMouseDown()
    {

        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }


    void OnMouseUp()
    {

        transform.position = new Vector3(gameObject.GetComponentInChildren<Transform>().position.x, gameObject.transform.position.y, gameObject.transform.position.z);

    }
}
