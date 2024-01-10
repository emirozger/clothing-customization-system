using System;
using UnityEngine;
using DG.Tweening;

public class CharacterRotator : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    private bool isRotating;
    public LayerMask characterMask;
    public Transform characterTransform;
    public SkinnedMeshRenderer characterMesh;
    public Material characterNakedMaterial;
    
    public Camera mainCamera;       // Reference to the camera to control
    public float zoomSpeed = 5.0f; // Zoom speed
    public float minZoom = 0.5f;  // Minimum zoom level
    public float maxZoom = 2.0f;  // Maximum zoom level
    private bool isZooming = false; //


    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleRotating();
        HandleNaked();
        
       
    }

    private void HandleRotating()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterMask))
            {
                if (hit.collider != null)
                {
                    isRotating = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
        
        if (isRotating)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
            characterTransform.Rotate(Vector3.up, -rotationX);
        }
    }

    private void HandleNaked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 1.0f);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterMask))
            {
                if (hit.collider != null)
                {
                    characterMesh.material = characterNakedMaterial;
                }
            }
        }
    }
}