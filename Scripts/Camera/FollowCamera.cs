using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private GameObject target;
    public Vector3 offset = new Vector3(5, 6.65f, 0);
    public Vector3 rotation = new Vector3(45f, -90f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = target.transform.position + offset;
    }

    public void FixRotation()
    {
        gameObject.transform.rotation = Quaternion.Euler(rotation);
    }
}
