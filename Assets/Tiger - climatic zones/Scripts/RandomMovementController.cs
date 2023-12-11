using UnityEngine;

public class RandomMovementController : MonoBehaviour
{
    
    private float speed;
    public float GetSpeed => speed;
    public void SetSpeed(float value) => speed = value;

    private Vector2 startCorner;
    private Vector2 endCorner;

    private bool isEndedThePath;
    private Vector2 pointToMove;
    private void Awake() {
        isEndedThePath = true;
    }

    public void SetCorners(Vector2 startCorner, Vector2 endCorner) {
        this.startCorner = startCorner;
        this.endCorner = endCorner;
    }

    private void Update() {
        if (startCorner == Vector2.zero && endCorner == Vector2.zero) return;

        if (IsVectors2Equals(transform.position, pointToMove)) {
            isEndedThePath = true;
        }
        if (isEndedThePath) {
            isEndedThePath = false;

            Vector2 setNewPath = GetRandomPosition();
            pointToMove = setNewPath;
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, pointToMove, step);
    }

    private bool IsVectors2Equals(Vector2 v1, Vector2 v2) {
        float delta = .05f;
        float minX = v1.x - delta;
        float maxX = v1.x + delta;

        float minY = v1.y - delta;
        float maxY = v1.y + delta;

        if (
            (minX < v2.x && v2.x < maxX)
            &&
            (minY < v2.y && v2.y < maxY)
        ) return true;

        return false;
    }

    private Vector2 GetRandomPosition() {

        float randX = Random.Range(startCorner.x, endCorner.x);
        float randY = Random.Range(startCorner.y, endCorner.y);
        Vector2 position = new(randX, randY);

        return position;
    }
}
