using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchSlot : MonoBehaviour
{
    public GameObject myPrefab;
    
    private void OnMouseUp() {
        Instantiate(myPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
