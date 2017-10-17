using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CassidyInteraction : MonoBehaviour {
    public List<GameObject> targets;

    private void Update()
    {
        if (GameState.Instance.currentState == GameState.State.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var target = targets.FirstOrDefault();
                if (target != null)
                {
                    foreach(var interact in target.GetComponents<Interaction>())
                    {
                        interact.Interact(gameObject);
                    }
                    RemoveTarget(target);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddTarget(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AddTarget(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveTarget(collision.gameObject);
    }

    private void AddTarget(GameObject other)
    {
        if (!targets.Contains(other))
        {
            if (other.GetComponent<Interaction>() != null)
            {
                targets.Add(other);
            }
        }
    }

    private void RemoveTarget(GameObject other)
    {
        targets.Remove(other);
    }
}
