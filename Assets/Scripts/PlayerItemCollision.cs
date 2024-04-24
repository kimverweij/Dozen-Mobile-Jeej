using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollision : MonoBehaviour
{
    // Start is called before the first frame update

    private DragDropManager m_DropManager;

    public void setCallBackFunction(DragDropManager dropManager)
    {
        m_DropManager = dropManager;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("OnTriggerEnter2D" + collision);
        m_DropManager.SetCollidedObject(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
      m_DropManager.ResetCollidedObject(collision.gameObject);
    }

}
