using UnityEngine;

public class CutHole : MonoBehaviour {

    public float SwirlAmount = 100f;
    public float StartSpeed = .5f;
    public float EndSpeed = 3f;

    Renderer imageRend;

    float desiredStrength;
    Camera mainCam;
    float SwirlSpeed = .5f;

    void Start() {
        mainCam = Camera.main;
        imageRend = GetComponent<Renderer>();
    }

    void Update() {
        desiredStrength = Mathf.Lerp(desiredStrength, 0, Time.deltaTime * SwirlSpeed);
        SetSwirlStrength(desiredStrength);
    }

    void SetSwirlStrength(float val) {
        imageRend.material.SetFloat("_SwirlStrength", val);
    }

    public void OnMouseDown() {
        desiredStrength = SwirlAmount;
        SetSwirlStrength(SwirlAmount);
        PositionSwirlToTouch();
        SwirlSpeed = StartSpeed;
    }

    public void OnMouseDrag() {
        PositionSwirlToTouch();
    }

    public void OnMouseUp() {
        SwirlSpeed = EndSpeed;
        PositionSwirlToTouch();
    }

    void PositionSwirlToTouch() {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            //convert hit from world to localspace
            Vector3 objectPos = transform.InverseTransformPoint(hit.point);
            //remap to shader coords: -.5-.5 to 0-1
            Vector4 shaderPos = Vector4.zero;
            shaderPos.x = objectPos.x + .5f;
            shaderPos.y = objectPos.y + .5f;
            imageRend.material.SetVector("_SwirlCenter", shaderPos);
        }
    }
}
