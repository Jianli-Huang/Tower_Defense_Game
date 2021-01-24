using UnityEngine;

public static class UtilityMethods
{
    public static void MoveUiElementToWorldPosition(RectTransform rectTransform, Vector3 worldPosition)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
        rectTransform.position = screenPoint;
    }

    public static Quaternion SmoothlyLook(Transform fromTransform, Vector3 toVector3)
    {
        if(fromTransform.position == toVector3)
        {
            return fromTransform.localRotation;
        }

        Quaternion currentRotation = fromTransform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(toVector3 - fromTransform.position);

        return Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 10f);
    }
}

