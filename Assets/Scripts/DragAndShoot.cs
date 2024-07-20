
using UnityEngine;


public class DragAndShoot : MonoBehaviour
{
    public Rigidbody rb;
    public float power = 4f;


    public Vector2 minPower;
    public Vector2 maxPower;
    public GameObject spritePrefab;
    public GameObject instanstiatedSprite;

    private Camera _camera;
    private Vector3 _force;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private LineTrail _trail;
    private Trajectory _trajectory;
    [SerializeField] private int steps;


    private bool _isGrounded;


    private void Start()
    {
        _camera = Camera.main;
        _trail = GetComponent<LineTrail>();
        _trajectory = GetComponentInChildren<Trajectory>();
    }


    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f);


        if (!_isGrounded)
        {
            _trail.EndLine();
            _trajectory.EndLine02();
            
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));
            rb.drag = 0;
        }


        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));
            _trail.RenderLine(currentPoint);


            _force = new Vector3(Mathf.Clamp(_startPoint.x - currentPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(_startPoint.y - currentPoint.y, minPower.y, maxPower.y), 0);


            Vector3[] trajectory = _trajectory.Plot(transform.position, _force * power, steps);
            _trajectory.RenderTrajectory(trajectory);
        }


        if (Input.GetMouseButtonUp(0))
        {
            _trail.EndLine();
            _trajectory.EndLine02();
            _endPoint = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));


            _force = new Vector3(Mathf.Clamp(_startPoint.x - _endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(_startPoint.y - _endPoint.y, minPower.y, maxPower.y), 0);
            // rb.AddForce(_force * power, ForceMode.Impulse);
            rb.velocity = _force * power;
            rb.useGravity = false;
            instanstiatedSprite = Instantiate(spritePrefab, _trajectory.tempVec + _force.normalized * 0.2f, Quaternion.identity);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gravity_trigger"))
        {
            rb.useGravity = true;
            Destroy(instanstiatedSprite);
            rb.drag = 2.5f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("bouncy"))
        {
            rb.useGravity = true;
            Destroy(instanstiatedSprite);
        }
    }


}
