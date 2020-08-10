using UnityEngine;

public class CutHole : MonoBehaviour {

    Renderer imageRend;

    float desiredStrength;
    float SwirlSpeed = .5f;
    float SwirlAmount = 100f;

    void Start() {
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
        SwirlSpeed = .5f;
    }

    public void OnMouseDrag() {
        PositionSwirlToTouch();
    }

    public void OnMouseUp() {
        SwirlSpeed = 3f;
        PositionSwirlToTouch();
    }

    void PositionSwirlToTouch() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

    public static float Remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
