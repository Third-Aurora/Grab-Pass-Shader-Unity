using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceContent : MonoBehaviour {

    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;

    Camera mainCam;

    void Start() {
        mainCam = Camera.main;
    }

    void Update() {

        if (Input.GetMouseButtonUp(0) && !IsClickOverUI() && IsClickOverPlane()) {
        
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            if (hitPoints.Count > 0) {
                Pose pose = hitPoints[0].pose;
                transform.rotation = pose.rotation;
                transform.position = pose.position;
            }
        }
    }

    bool IsClickOverPlane() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            return hit.collider.gameObject.CompareTag("plane");
        }
        return false;
    }

    bool IsClickOverUI() {
        //dont place content if pointer is over ui element
        PointerEventData data = new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Count > 0;
    }
}
