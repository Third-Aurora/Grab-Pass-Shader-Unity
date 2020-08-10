using UnityEngine;

public class CutHole : MonoBehaviour {

    Renderer imageRend;

    void Start() {
        imageRend = GetComponent<Renderer>();
    }

    public void OnMouseDrag() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            //convert hit from world to localspace
            Vector3 objectPos = transform.InverseTransformPoint(hit.point);
            //remap to shader coords from 0-1 to -.5-.5
            Vector4 shaderPos = Vector4.zero;
            shaderPos.x = objectPos.x + .5f;
            shaderPos.y = objectPos.y + .5f;
            imageRend.material.SetVector("_SwirlCenter", shaderPos);
        }
    }
}
