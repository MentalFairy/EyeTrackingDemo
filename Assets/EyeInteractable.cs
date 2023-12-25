using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    [field:SerializeField]
    public bool IsHovered { get; internal set; }

    [SerializeField]
    UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    Material OnHoverMaterial;

    [SerializeField]
    Material OnHoverInactiveMaterial;

    [SerializeField]
    MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHovered)
        {
            meshRenderer.material = OnHoverMaterial;
            OnObjectHover?.Invoke(gameObject);
        }
        else
        {
            meshRenderer.material = OnHoverInactiveMaterial;
        }
    }
}
