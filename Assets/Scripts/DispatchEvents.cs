using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DispatchEvents : MonoBehaviour
{
    public UnityEvent<GameObject> onCollisionEvent  = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> ExitCollsionEvent = new UnityEvent<GameObject>();
}
