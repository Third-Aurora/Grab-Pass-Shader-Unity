using UnityEngine;

public class Animate : MonoBehaviour {

    Vector3 endPos = new Vector3(0, 0, -1f);

    Vector3 startPos;
    Vector3 desiredPos;

    void Start() {
        startPos = transform.localPosition;
        desiredPos = startPos;
    }

    // Update is called once per frame
    void Update() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, Time.deltaTime * 2f);

        if (desiredPos == startPos && Vector3.Distance(transform.localPosition,startPos) < .1f) {
            desiredPos = endPos;
        }

        if (desiredPos == endPos && Vector3.Distance(transform.localPosition, endPos) < .1f) {
            desiredPos = startPos;
        }
    }
}
