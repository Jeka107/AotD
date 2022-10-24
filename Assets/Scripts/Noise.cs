using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Noise : MonoBehaviour
{
    public delegate void OnFirstInteraction(string state);
    public static event OnFirstInteraction onFirstInteraction;

    [SerializeField] private Material noiseMat;

    public bool isMakingNoise = false;
    private float noiseRadius;
    private bool isHearing = false;
    private bool firstInteraction = false;

    private LineRenderer noiseLineRenderer;
    [SerializeField] private float noiseLineWidth;
    [SerializeField] private int noiseLineResolution;

    private void Awake()
    {
        noiseLineRenderer = GetComponent<LineRenderer>();
        noiseLineRenderer.material = noiseMat;
        noiseLineRenderer.startWidth = noiseLineWidth;
        noiseLineRenderer.endWidth = noiseLineWidth;
        noiseLineRenderer.enabled = false;
    }

    public void SetNoise(bool _isMakingNoise)
    {
        isMakingNoise = _isMakingNoise;
    }

    public void SetNoise(bool _isMakingNoise, float _noiseRadius)
    {
        isMakingNoise = _isMakingNoise;
        noiseRadius = _noiseRadius;
    }
    public bool GetIsHearing()
    {
        return isHearing;
    }

    private void Update()
    {
        if (isMakingNoise)
        {
            DrawNoiseArea();
            EmitNoise();
        }
        else
        {
            noiseLineRenderer.enabled = false;
            isHearing = false;
        }
    }

    private void EmitNoise()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, noiseRadius);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.TryGetComponent<Ears>(out Ears ears))
            {
                isHearing = true;
                if(!firstInteraction)
                {
                    //event
                    onFirstInteraction?.Invoke("Crouch");
                    firstInteraction = true;
                }
                return;
            }
        }
        isHearing = false;
    }

    private void DrawNoiseArea()
    {
        noiseLineRenderer.enabled = true;

        if (noiseRadius > 0)
        {
            noiseLineRenderer.positionCount = noiseLineResolution + 1;

            float angle = 0;

            for (int i = 0; i < noiseLineResolution; i++)
            {
                angle = i * (360 / noiseLineResolution);

                float x = transform.position.x + noiseRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
                float y = transform.position.z + noiseRadius * Mathf.Cos(angle * Mathf.Deg2Rad);

                Vector3 vertexPos = new Vector3(x, noiseLineWidth / 2, y);

                noiseLineRenderer.SetPosition(i, vertexPos);

                if (i == 0)
                {
                    noiseLineRenderer.SetPosition(noiseLineResolution, vertexPos);
                }
            }
        }
    }
}
