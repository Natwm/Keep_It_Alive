using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Param
    [Header ("Field Of View")]
    [SerializeField]private float viewRadius;

    [Range (0,360)]
    [SerializeField] private float viewAngle;

    [Range(0, 1)]
    [SerializeField] private float meshResolution;

    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    [Space]
    [Header ("Time Between two Raycast")]
    [SerializeField] private float delay;

    [Space]
    [Header("Target")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

     private List<Transform> visibleTarget = new List<Transform>();
     private List<GameObject> visibleGameobject = new List<GameObject>();
    #endregion

    #region UPDATE
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";

        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetWithDelay", delay);
    }
    private void LateUpdate()
    {
        DrawFieldOfView();
    }
    #endregion


    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTarget();
        }
    }

    #region FindTarget
    public Vector3 DirFromAngle( float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3( Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void FindTarget()
    {
        visibleTarget.Clear();
        visibleGameobject.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius,targetMask);
        Debug.Log(targetsInRadius.Length);
         for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                visibleGameobject.Add(targetsInRadius[i].gameObject);
                RaycastHit hitInfo;
                if(!Physics.Raycast(transform.position,dirToTarget, out hitInfo, distanceToTarget, obstacleMask))
                {
                    visibleTarget.Add(target);
                }
                    
            }
        }
         if(visibleGameobject.Count != 0)
        {
            UpdateSpotColor();
        }
    }

    private void UpdateSpotColor()
    {
        foreach (GameObject spot in visibleGameobject)
        {
            spot.GetComponent<PixelBehaviours>().UpdateValues(gameObject.tag);
        }
    }

    #endregion

        void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoint = new List<Vector3>();

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoint.Add(newViewCast.point);
        }

        int vertexCount = viewPoint.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            Debug.Log("transform.InverseTransformPoint(viewPoint[i]); " + transform.InverseTransformPoint(viewPoint[i]));
            vertices[i + 1] = transform.InverseTransformPoint(viewPoint[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    
    ViewCastInfo ViewCast (float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir,out hit, ViewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;
        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }


        #region GETTER && SETTER
        public float ViewAngle { get => viewAngle; set => viewAngle = value; }
    public LayerMask TargetMask { get => targetMask; set => targetMask = value; }
    public List<Transform> VisibleTarget { get => visibleTarget; set => visibleTarget = value; }
    public float ViewRadius { get => viewRadius; set => viewRadius = value; }
    #endregion


}
