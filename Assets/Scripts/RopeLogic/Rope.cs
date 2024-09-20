using UnityEngine;

public class Rope : MonoBehaviour
{

    public GameObject connectionPoint;
    Transform toLookAt;
    public Vector3 lookAtOffset;
   
    void Start()
    {
        if (PersistentManager.Instance != null)
        {
            toLookAt = PersistentManager.Instance.GetPlayer().transform;
        }
    }

    void LateUpdate()
    {
        if (connectionPoint && toLookAt != null)
        {
            GetComponent<LineRenderer>().SetPosition(0, connectionPoint.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, Vector3.MoveTowards(connectionPoint.transform.position, toLookAt.position + lookAtOffset, 2));
        }
    
    }

    void OnEnable()
    {
        GetComponent<LineRenderer>().SetPosition(0,Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1,Vector3.zero);
    }

}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
 public struct RopeSection
    {
        public Vector3 pos;

        public Vector3 oldPos;

        public static readonly RopeSection zero = new RopeSection(Vector3.zero);

        public RopeSection(Vector3 pos)
        {
            this.pos = pos;
            oldPos = pos;
        }
    }
    public Transform whatTheRopeIsConnectedTo;

    public Transform whatIsHangingFromTheRope;

    public Vector3 lastposition;


    public float ropeWidth;

    public float ropeSectionLength = 0.5f;

    public bool simulate = true;
    [Tooltip("This times ropeSectionLength will give the treshold")]
    public float distanceTreshold;

    private LineRenderer lineRenderer;

    private List<RopeSection> allRopeSections = new List<RopeSection>();
    // Start is called before the first frame update
    void Start()
    {
        whatTheRopeIsConnectedTo  = transform;
        whatIsHangingFromTheRope = PersistentManager.Instance.GetPlayer().transform;
        lineRenderer = GetComponent<LineRenderer>();
        
        Vector3 position = whatTheRopeIsConnectedTo.position;
        for (int i = 0; i < 20; i++)
        {
            allRopeSections.Add(new RopeSection(position));
            position.y -= ropeSectionLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(whatIsHangingFromTheRope == null || whatTheRopeIsConnectedTo == null)
        {
            lineRenderer.enabled = false;
            return;
        }
        if (lineRenderer.enabled)
        {
            DisplayRope();
        }
        if (Vector3.Distance(transform.position, lastposition) > 2.0f)
        {
            Teleport();
        }
        lastposition = transform.position;
    }
    private void FixedUpdate()
    {
       if(whatIsHangingFromTheRope == null || whatTheRopeIsConnectedTo == null)
        {
            lineRenderer.enabled = false;
            return;
        }
         UpdateRopeSimulation();

    }
    public void Teleport()
    {
        simulate = false;
        for (int i = 0; i < 400; i++)
        {
            UpdateRopeSimulation();
        }
        simulate = true;
    }


    private void UpdateRopeSimulation()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        if (!simulate)
        {
            fixedDeltaTime = 0.01f;
        }
        Vector3 a = new Vector3(0f, -0.1f, 0f);
        
        RopeSection value = allRopeSections[0];
        value.pos = whatTheRopeIsConnectedTo.position;
        allRopeSections[0] = value;
        RopeSection value2 = allRopeSections[allRopeSections.Count - 1];
        value2.pos = whatIsHangingFromTheRope.position - new Vector3(0f, 0.5f, 0f);
        allRopeSections[allRopeSections.Count - 1] = value2;
        for (int i = 1; i < allRopeSections.Count - 1; i++)
        {
            RopeSection value3 = allRopeSections[i];
            Vector3 vector = value3.pos - value3.oldPos;
            value3.oldPos = value3.pos;
            value3.pos += vector;
            value3.pos += a * fixedDeltaTime;
            allRopeSections[i] = value3;
        }
        for (int j = 0; j < 20; j++)
        {
            ImplementMaximumStretch();
        }
    }
    private void ImplementMaximumStretch()
    {
        for (int i = 0; i < allRopeSections.Count - 1; i++)
        {
            RopeSection value = allRopeSections[i];
            RopeSection value2 = allRopeSections[i + 1];
            float magnitude = (value.pos - value2.pos).magnitude;
            float d = Mathf.Abs(magnitude - ropeSectionLength);
            Vector3 zero = Vector3.zero;
            if (magnitude > ropeSectionLength)
            {
                zero = (value.pos - value2.pos).normalized;
            }
            else
            {
                if (!(magnitude < ropeSectionLength))
                {
                    continue;
                }
                zero = (value2.pos - value.pos).normalized;
            }
            Vector3 vector = zero * d;
            if (i != 0 || i != allRopeSections.Count - 1)
            {
                value2.pos += vector * 0.5f;
                allRopeSections[i + 1] = value2;
                value.pos -= vector * 0.5f;
                allRopeSections[i] = value;
            }
            else
            {
                value2.pos += vector;
                allRopeSections[i + 1] = value2;
            }
        }
    }

    private void DisplayRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] array = new Vector3[allRopeSections.Count+2];
        array[0] = whatTheRopeIsConnectedTo.transform.position;
        for (int i = 1; i < allRopeSections.Count+1; i++)
        {
            array[i] = allRopeSections[i-1].pos;
        }
        array[allRopeSections.Count + 1] = whatIsHangingFromTheRope.transform.position- new Vector3(0f, 0.5f, 0f);
        lineRenderer.positionCount = array.Length;
        lineRenderer.SetPositions(array);
    }

   


}
*/