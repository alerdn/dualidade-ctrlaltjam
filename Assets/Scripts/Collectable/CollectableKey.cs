using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : CollectableBase
{
    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
