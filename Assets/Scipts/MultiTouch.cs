using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase.Equals(TouchPhase.Began))
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out var hit))
                {
                    GameObject gameObject = hit.transform.gameObject;
                    if (gameObject.GetComponent<Stone>())
                    {
                        gameObject.GetComponent<Stone>().DestroyObject();
                    }
                }
            }
        }
    }
}
