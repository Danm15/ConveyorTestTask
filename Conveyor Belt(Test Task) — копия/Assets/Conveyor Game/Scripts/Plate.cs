using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Plate : MonoBehaviour
{
    [SerializeField] private List<Transform> _plateSlots;
    
    
    public Transform FreeSlot()
    {
        Transform freeSlot;
        int randomSlot = Random.Range(0, _plateSlots.Count);
        freeSlot = _plateSlots[randomSlot];
        _plateSlots.Remove(_plateSlots[randomSlot]);
        return freeSlot;
    }
}
