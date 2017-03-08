﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CarMovement : MonoBehaviour
{

    GameObject car;


    void Start()
    {
        car = this.gameObject;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        if (hit.collider.gameObject.tag == "Lane")
                        {
                            Transform lane = hit.collider.transform;
                            car.transform.position = lane.position;
                        }
                    }
                }
            }
        }
        else
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.collider.gameObject.tag == "Lane")
                        {
                            Transform lane = hit.collider.transform;
                            car.transform.position = lane.position;
                        }
                    }
                }
            }
        }
    }



}


