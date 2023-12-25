using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EyeTrackingRay : MonoBehaviour
{
    [SerializeField]
    float rayDistance = 1f;
    [SerializeField]
    float rayWidth = 0.01f;

    [SerializeField]
    LayerMask layersToInclude;
    [SerializeField]
    Color rayColorDefaultState = Color.yellow;

    [SerializeField]
    Color raycolorHover = Color.red;

    LineRenderer lineRenderer;

    [SerializeField]
    List<EyeInteractable> eyeInteractables = new();

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer= GetComponent<LineRenderer>();
        SetupRay();
    }

    private void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        lineRenderer.startColor = rayColorDefaultState;
        lineRenderer.endColor = rayColorDefaultState;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + rayDistance));
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        var castDir = transform.TransformDirection(Vector3.forward) * rayDistance;

        if(Physics.Raycast(transform.position, castDir, out hit, Mathf.Infinity, layersToInclude))
        {
            Unselect(false);
            lineRenderer.startColor = raycolorHover;
            lineRenderer.endColor = raycolorHover;
            var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
            eyeInteractables.Add(eyeInteractable);
            eyeInteractable.IsHovered = true;
        }
        else
        {
            lineRenderer.startColor = rayColorDefaultState;
            lineRenderer.endColor = rayColorDefaultState;
            Unselect(true);
        }
    }

    private void Unselect(bool clear = false)
    {
        foreach (var eyeInteractable in eyeInteractables)
        {
            eyeInteractable.IsHovered = false;
        }
        if (clear)
        {
            eyeInteractables.Clear();
        }
    }

}
