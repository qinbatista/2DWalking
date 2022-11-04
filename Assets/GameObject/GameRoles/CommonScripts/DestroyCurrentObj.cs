using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCurrentObj : MonoBehaviour
{
    void DestroyCurrentObject()
    {
        GameObject current = gameObject.transform.gameObject;
        Destroy(current);
    }

}
