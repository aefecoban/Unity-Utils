using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowerCamera : MonoBehaviour
{

    [Header("Follow")]
    [SerializeField] GameObject target;
    [SerializeField] bool xAxisFollow = true;
    [SerializeField] bool yAxisFollow = false;
    [SerializeField] bool followTarget = true;
    [Tooltip("PlayerSize %")]
    [SerializeField] [Range(0, 200)] float Epsilon = 0f;
    [Space]
    [Header("Areas")]
    [SerializeField] [Range(0, 50)] float LeftTargetPoint   = 0f;
    [SerializeField] [Range(0, 50)] float RightTargetPoint  = 0f;
    [SerializeField] [Range(0, 50)] float TopTargetPoint    = 0f;
    [SerializeField] [Range(0, 50)] float BottomTargetPoint = 0f;
    [SerializeField] bool CameraXLimit = true;
    [SerializeField] bool CameraYLimit = true;
    [SerializeField] float camMinY = -5f;
    [SerializeField] float camMinX = -10f;
    [SerializeField] float camMaxX = 33f;
    [Space]
    [Header("Editor")]
    [SerializeField] Color CamPointColor = new Color(8f, 0.2f, 0.2f, 0.5f);
    [SerializeField] Color CamEdgeColor = new Color(8f, 0.2f, 0.2f, 1f);
    [SerializeField] Color SafeAreaEdge = new Color(0.2f, 0.8f, 0.2f, 0.5f);
    [SerializeField] Color TargetArea = new Color(0.2f, 0.5f, 0.5f, 0.5f);

    private Camera cam;
    private Vector3 position = Vector3.one;
    private Vector3 targetPosition = Vector3.one;

    private Vector2 topLeft = Vector2.zero;
    private Vector2 topRight = Vector2.zero;
    private Vector2 bottomLeft = Vector2.zero;
    private Vector2 bottomRight = Vector2.zero;

    private Vector2 safeAreaTopLeft = Vector2.zero;
    private Vector2 safeAreaTopRight = Vector2.zero;
    private Vector2 safeAreaBottomLeft = Vector2.zero;
    private Vector2 safeAreaBottomRight = Vector2.zero;

    private float widthUnit = 0f;
    private float heightUnit = 0f;
    private float widthPixel = 0f;
    private float heightPixel = 0f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        FindCamEdgesOnWorld();
        float x = target.gameObject.transform.position.x;
        float y = target.gameObject.transform.position.y;
        if (CameraYLimit)
        {
            float camMinCenterY = camMinY;
            camMinCenterY += (topRight.y - bottomRight.y) / 2f;
            y = Mathf.Max(y, camMinCenterY);
        }
        if (CameraXLimit)
        {
            float camMinCenterX = camMinX;
            float camMaxCenterX = camMaxX;
            camMinCenterX += (topRight.x - bottomLeft.x) / 2f;
            camMaxCenterX -= (topRight.x - bottomLeft.x) / 2f;
            x = Mathf.Max(x, camMinCenterX);
            x = Mathf.Min(x, camMaxCenterX);
        }
        
        //this.transform.position = (new Vector3(x, y, cam.gameObject.transform.position.z));
        
        if (followTarget)
        {
            FindCamEdgesOnWorld();
            Follow();
        }
    }

    void FindCamEdgesOnWorld()
    {
        position            = this.transform.position;
        targetPosition      = (target != null) ? target.transform.position : Vector3.one;
        topLeft             = Vector2.zero;
        topRight            = Vector2.zero;
        bottomLeft          = Vector2.zero;
        bottomRight         = Vector2.zero;

        topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        topLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        bottomRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        widthPixel = cam.pixelWidth;
        heightPixel = cam.pixelHeight;
        widthUnit = topRight.x - topLeft.x;
        heightUnit = topRight.y - bottomRight.y;

        float minX = topLeft.x + (LeftTargetPoint * widthUnit / 100), maxX = topRight.x - (RightTargetPoint * widthUnit / 100);
        float minY = bottomLeft.y + (BottomTargetPoint * heightUnit / 100), maxY = topLeft.y - (TopTargetPoint * heightUnit / 100);

        safeAreaTopLeft = new Vector2(minX, maxY);
        safeAreaTopRight = new Vector2(maxX, maxY);
        safeAreaBottomLeft = new Vector2(minX, minY);
        safeAreaBottomRight = new Vector2(maxX, minY);
    }

    private void Follow()
    {
        if (!followTarget || (!xAxisFollow && !yAxisFollow))
        return;

        Vector2 targetPos = (target != null) ? target.transform.position : Vector3.one;
        bool xAxis = xAxisFollow;
        bool yAxis = yAxisFollow;

        float epsilonSize = 0;
        if(target.GetComponent<Collider2D>() != null)
            epsilonSize = target.GetComponent<Collider2D>().bounds.size.x * Epsilon / 100f;

        if (xAxisFollow)
            if (targetPos.x - epsilonSize > safeAreaBottomLeft.x && targetPos.x + epsilonSize < safeAreaBottomRight.x)
                xAxis = false;

        if (yAxisFollow)
            if (targetPos.y > safeAreaBottomLeft.y && targetPos.y < safeAreaTopRight.y)
                yAxis = false;

        if (!xAxis && !yAxis)
        return;

        Vector3 nPosCam = cam.transform.position;

        if (xAxis && xAxisFollow)
            nPosCam.x = Mathf.Lerp(nPosCam.x, targetPos.x, Time.deltaTime);

        if (yAxis && yAxisFollow)
            nPosCam.y = Mathf.Lerp(nPosCam.y, targetPos.y, Time.deltaTime);

        cam.gameObject.transform.position = nPosCam;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color        = CamPointColor;

        Gizmos.DrawCube(topLeft,    Vector3.one / 2f);
        Gizmos.DrawCube(topRight,   Vector3.one / 2f);
        Gizmos.DrawCube(bottomLeft, Vector3.one / 2f);
        Gizmos.DrawCube(bottomRight,Vector3.one / 2f);

        Gizmos.color = CamEdgeColor;

        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);

        Gizmos.color = SafeAreaEdge;

        Gizmos.DrawLine(safeAreaTopRight, safeAreaTopLeft);
        Gizmos.DrawLine(safeAreaTopLeft, safeAreaBottomLeft);
        Gizmos.DrawLine(safeAreaBottomLeft, safeAreaBottomRight);
        Gizmos.DrawLine(safeAreaBottomRight, safeAreaTopRight);

        Gizmos.color = TargetArea;

        Vector2 targetPos = (target != null) ? target.transform.position : Vector3.one;
        float epsilonSize = 0;
        if (target.GetComponent<Collider2D>() != null)
            epsilonSize = target.GetComponent<Collider2D>().bounds.size.x * Epsilon / 100f;

        Vector2 targetArea = new Vector2((((targetPos.x - epsilonSize) + (targetPos.x + epsilonSize)) /2f), targetPos.y);
        Vector2 targetSize = new Vector2(epsilonSize * 2f, 1f);

        Gizmos.DrawCube(targetArea, targetSize);

    }

}
