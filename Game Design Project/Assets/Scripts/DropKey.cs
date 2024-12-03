using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKey : MonoBehaviour
{
    public GameObject key;
    private SkeletonTest skelly;
    private bool dropped = false;

    void Start()
    {
        skelly = GetComponent<SkeletonTest>();
    }

    void Update()
    {
        if (skelly.IsDeath())
        {
            Drop(skelly.GetPos());
        }
    }
    // Start is called before the first frame update
    public void Drop(Vector3 position)
    {
        if (!dropped)
        {
            GameObject keydrop = Instantiate(key, position, Quaternion.identity);
        }
        dropped = true;
        return;
    }
}
