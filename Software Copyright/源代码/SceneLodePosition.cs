using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLodePosition : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Manager manager;
    public Transform transform1;
    public Transform transform2;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
        transform1 = manager.transform.GetChild(0).GetChild(0);
        transform2 = manager.transform.GetChild(1);
        transform1.position = point1.position;
        transform1.rotation = point1.rotation;
        transform2.position = point2.position;
        transform2.rotation = point2.rotation;
        manager.CloseAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
