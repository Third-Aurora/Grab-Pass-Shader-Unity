using UnityEngine;

public class CamTest : MonoBehaviour {

    public Renderer quad;

    WebCamTexture webCamTexture;

    void Start() {
        webCamTexture = new WebCamTexture();
        quad.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    void Update() {
        transform.localScale = new Vector3(webCamTexture.width, webCamTexture.height, 1);
    }
}
