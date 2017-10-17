using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AbstractCreateObject : MonoBehaviour, InheritCollisionIgnore
{
    public GameObject createThis;
    public int numberToCreate;
    public bool createAsChild;
    public bool impartVelocity;
    public bool impartRotation;
    public bool impartScale;

    protected bool doNotCreate = false;
    private GameObject createdInstance;
    private List<GameObject> ignoreCollisions = new List<GameObject>();

    private Vector2 savedVelocity;

    [HideInInspector]
    public float inheritVelocity;

    [HideInInspector]
    public float addRandomImpulse;

    private void FixedUpdate()
    {
        if (impartVelocity)
        {
            var myBody = GetComponent<Rigidbody2D>();
            savedVelocity = myBody.velocity;
        }
    }

    protected List<GameObject> CreateObjects(Vector3 position)
    {
        if (doNotCreate || GameState.Instance.currentState == GameState.State.MENU)
        {
            return null;
        }
        var created = new List<GameObject>();
        for (int i = 0; i < numberToCreate; i++)
        {
            createdInstance = GameObject.Instantiate(createThis);
            createdInstance.transform.position = position;
            if (impartRotation)
            {
                createdInstance.transform.rotation = gameObject.transform.rotation;
            }
            if (impartVelocity)
            {
                var body = createdInstance.GetComponent<Rigidbody2D>();
                if (body != null)
                {
                    body.velocity = (savedVelocity * inheritVelocity) + (Random.insideUnitCircle * addRandomImpulse);
                }
            }
            if (impartScale)
            {
                createdInstance.transform.localScale = gameObject.transform.localScale;
            }
            if (ignoreCollisions.Count > 0)
            {
                var myBody = createdInstance.GetComponent<Collider2D>();
                foreach (var ignore in ignoreCollisions)
                {
                    var ignoreBody = ignore.GetComponents<Collider2D>();
                    foreach (var body in ignoreBody)
                    {
                        Physics2D.IgnoreCollision(myBody, body);
                    }
                    var generator = createdInstance.GetComponent<CreateObjectOnDestroy>();
                    if (generator != null)
                    {
                        generator.IgnoreCollisions(ignore);
                    };
                }
            }
            if (createAsChild)
            {
                createdInstance.transform.SetParent(gameObject.transform, true);
            }
            created.Add(createdInstance);
        }
        return created;
    }

    private void OnApplicationQuit()
    {
        doNotCreate = true;
    }

    public void IgnoreCollisions(GameObject ignore)
    {
        ignoreCollisions.Add(ignore);
    }


    [CustomEditor(typeof(AbstractCreateObject))]
    public class AbstractCreateObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CreateObjectOnDestroy script = (CreateObjectOnDestroy)target;
            if (script.impartVelocity)
            {
                script.inheritVelocity = EditorGUILayout.Slider("Inherit Velocity", script.inheritVelocity, 0f, 1f);
                script.addRandomImpulse = EditorGUILayout.Slider("AddRandomImpulse", script.addRandomImpulse, 0f, 20f);
            }
        }
    }
}
