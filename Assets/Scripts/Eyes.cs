using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(MeshRenderer))]
public class Eyes : MonoBehaviour
{
    public delegate void OnFirstInteraction(string state);
    public static event OnFirstInteraction onFirstInteraction;

    [Header("Player")]
    [SerializeField] private PlayerActions playerActions;

    [Header("Sight")]
    [SerializeField] private float sightAngle;
    private float startSightRange;
    [SerializeField] private float sightRange;
    [SerializeField] private float scoutRangeAngle;
    [SerializeField] private float scoutRotationSpeed;

    [SerializeField] [Range(0.01f, 1f)] private float meshResolution;
    [SerializeField] private float meshHight;

    [SerializeField] private LayerMask obstaclesMask;

    private MeshFilter sightMeshFilter;
    private Mesh sightMesh;

    private bool isLooking = true;
    private bool isSeeingPlayer = false;
    private bool firstInteraction = false;

    private void Awake()
    {
        sightMeshFilter = GetComponent<MeshFilter>();
    }
    private void Start()
    {
        sightMesh = new Mesh();
        sightMesh.name = "sight";
        sightMeshFilter.mesh = sightMesh;
        startSightRange = sightRange;
        playerActions.flashLight += FlashLightStatus;
    }
    private void OnDestroy()
    {
        playerActions.flashLight -= FlashLightStatus;
    }

    public void SetIsLooking(bool _isLooking)
    {
        isLooking = _isLooking;
    }
    public bool GetIsSeeingPlayer()
    {
        return isSeeingPlayer;
    }

    private void Update()
    {
        if(isLooking)
        {
            DrawFieldOfView();
            SearchForPlayer();
        }
    }

    private void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(sightAngle * meshResolution);
        float rayAngleSize = sightAngle / rayCount;

        List<Vector3> endPoints = new List<Vector3>();

        for(int i=0;i<=rayCount;i++)
        {
            float angle = transform.eulerAngles.y - sightAngle / 2 + rayAngleSize * i;

            Vector3 rayEndPos = transform.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad))*sightRange;

            rayEndPos = GetRealEndPoint(rayEndPos);
            endPoints.Add(rayEndPos);
        }
        int vertexCount = endPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] trianges = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        vertices[0].y = meshHight;

        for(int i=0;i<vertexCount-1;i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(endPoints[i]);
            vertices[i + 1].y = meshHight;

            if(i<vertexCount-2)
            {
                trianges[i * 3] = 0;
                trianges[i * 3 + 1] = i + 1;
                trianges[i * 3 + 2] = i + 2;
            }
        }
        sightMesh.Clear();
        sightMesh.vertices = vertices;
        sightMesh.triangles = trianges;
        sightMesh.RecalculateNormals();
    }
    private void SearchForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange);

        foreach (Collider hit in hitColliders)
        {
            Vector3 distance = hit.transform.position - transform.position;
            distance.y = 0;
            float angle = Vector3.Angle(transform.forward, distance);

            if (angle <sightAngle/2)
            {
                if(hit.TryGetComponent<Targetable>(out Targetable player))
                {
                    if(player.GetIsSafe()==false)
                    {
                        isSeeingPlayer = true;
                        if(!firstInteraction)
                        {
                            //event
                            onFirstInteraction?.Invoke("Sprint");
                            firstInteraction = true;
                        }
                        return;
                    }
                }
            }
        }
        isSeeingPlayer = false;
    }
    private Vector3 GetRealEndPoint(Vector3 endPoint)
    {
        Vector3 direction = endPoint - transform.position;

        RaycastHit hit;

        if(Physics.Raycast(transform.position,direction,out hit,sightRange,obstaclesMask))
        {
            return hit.point;
        }
        return endPoint;
    }
    private void FlashLightStatus(bool status)
    {
        if (!status)
        {
            sightRange = sightRange / 2;
        }
        else
        {
            sightRange = startSightRange;
        }
    }
}
