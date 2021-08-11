using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{

    [SerializeField] private Transform target;

    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPos = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, (Time.deltaTime * speed));
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles);
    }
}
