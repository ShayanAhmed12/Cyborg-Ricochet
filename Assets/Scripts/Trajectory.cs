using UnityEngine;
public class Trajectory : MonoBehaviour
{
    private Rigidbody _rb;
    private LineRenderer _lr;
    public Vector3 tempVec;
    bool flag;
    public int bounceCount;

    Color c1 = new Color(1f, 0.54f, 0f, 1);
    Color c2 = Color.magenta;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _lr = GetComponent<LineRenderer>();
        _lr.numCapVertices = 40;
        _lr.startWidth = 0.3f;
        _lr.endWidth = 0.03f;
    }


    public Vector3[] Plot(Vector3 pos, Vector3 force, int steps)
    {
        bounceCount = 0;
        Vector3[] results = new Vector3[steps];
        float timeStep = Time.fixedDeltaTime;

        Vector3 moveStep = force * timeStep;

        for (int i = 0; i < steps; i++)
        {

            RaycastHit hit;
            
            if (Physics.Raycast(pos, moveStep, out hit, moveStep.magnitude))
            {

                if (hit.collider.CompareTag("bouncy"))
                {
                    moveStep = Vector3.Reflect(moveStep, hit.normal);
                    Debug.DrawLine(hit.point, force, Color.red);
                    bounceCount++;
                }
                else 
                {
                    moveStep = Vector3.zero;
                }
            }
            pos += moveStep;
            results[i] = pos;


        }

        if (bounceCount > 0)
        {
            _lr.startColor = c2;
        }
        else
        {
            _lr.startColor = c1;
        }
        //Debug.Log("Bounce count: " + bounceCount);

        tempVec = pos;

        return results;
    }


    public void RenderTrajectory(Vector3[] trajectory)
    {
        _lr.positionCount = trajectory.Length;
        _lr.SetPositions(trajectory);
    }


    public void EndLine02()
    {
        _lr.positionCount = 0;
    }
}
