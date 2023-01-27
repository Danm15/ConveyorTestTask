using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dump : MonoBehaviour
{
    private Queue<GameObject> _objectsInDump = new Queue<GameObject>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {

            _objectsInDump.Enqueue(other.gameObject);
            if(_objectsInDump.Count > 11)
            {
                _objectsInDump.Dequeue().gameObject.SetActive(false);
            }

        }
        
    }

    
}
