using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class Light : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.RotateAround(transform.parent.position, transform.parent.right, 0.5f);
        transform.LookAt(transform.parent.position);
    }
}
