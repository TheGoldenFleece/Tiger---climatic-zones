using UnityEngine;

public class PointAdapter : MonoBehaviour
{
    [SerializeField] private float percentageIndentX;
    [SerializeField] private float percentageIndentY;
    [SerializeField] private Corner corner;
    private enum Corner {
        LeftBottom, LeftTop, RightTop, RightBottom 
    }

    private void Awake() {

        Vector2 shift;
        shift.x = ResolutionAdapter.GetScreenToWorldWidth * percentageIndentX / 100;
        shift.y = ResolutionAdapter.GetScreenToWorldHeight * percentageIndentY / 100;

        switch (corner) {
            case Corner.LeftBottom: {
                    transform.position = GetLeftBottomRelativeVector(shift);
                    break;
                }
            case Corner.LeftTop: {
                    transform.position = GetLeftTopRelativeVector(shift);
                    break;
                }
            case Corner.RightTop: {
                    transform.position = GetRightTopRelativeVector(shift);
                    break;
                }
            case Corner.RightBottom: {
                    transform.position = GetRightBottomRelativeVector(shift);
                    break;
                }
        }
    }

    private Vector2 GetLeftBottomRelativeVector(Vector2 shift) {
        Vector2 leftBottomCorner = Vector2.zero;

        leftBottomCorner.x -= ResolutionAdapter.GetScreenToWorldWidth / 2;
        leftBottomCorner.y -= ResolutionAdapter.GetScreenToWorldHeight / 2;

        Vector2 relativePosition = shift + leftBottomCorner;

        return relativePosition;
    }

    private Vector2 GetLeftTopRelativeVector(Vector2 shift) {
        Vector2 leftTopCorner = Vector2.zero;

        leftTopCorner.x -= ResolutionAdapter.GetScreenToWorldWidth / 2;
        leftTopCorner.y += ResolutionAdapter.GetScreenToWorldHeight / 2;

        Vector2 relativePosition = Vector2.zero;

        relativePosition.x = leftTopCorner.x + shift.x;
        relativePosition.y = leftTopCorner.y - shift.y;

        return relativePosition;
    }

    private Vector2 GetRightTopRelativeVector(Vector2 shift) {
        Vector2 rightTopCorner = Vector2.zero;

        rightTopCorner.x += ResolutionAdapter.GetScreenToWorldWidth / 2;
        rightTopCorner.y += ResolutionAdapter.GetScreenToWorldHeight / 2;

        Vector2 relativePosition = Vector2.zero;

        relativePosition.x = rightTopCorner.x - shift.x;
        relativePosition.y = rightTopCorner.y - shift.y;

        return relativePosition;
    }

    private Vector2 GetRightBottomRelativeVector(Vector2 shift) {
        Vector2 rightBottomCorner = Vector2.zero;

        rightBottomCorner.x += ResolutionAdapter.GetScreenToWorldWidth / 2;
        rightBottomCorner.y -= ResolutionAdapter.GetScreenToWorldHeight / 2;

        Vector2 relativePosition = Vector2.zero;

        relativePosition.x = rightBottomCorner.x - shift.x;
        relativePosition.y = rightBottomCorner.y + shift.y;

        return relativePosition;
    }

    
}
