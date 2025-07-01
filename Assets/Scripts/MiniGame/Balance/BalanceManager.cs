using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BalanceBar : MonoBehaviour
{
    public Transform poidCookie;
    public Transform poidInsuline;
    public Transform bar;

    public float anglePerDisk = 3f;
    public float maxAngle = 15f;
    public float rotationSpeed = 5f;
    public float poidYOffsetMultiplier = 0.2f;

    private Vector3 poidCookieStartPos;
    private Vector3 poidInsulineStartPos;

    void Start()
    {
        poidCookieStartPos = poidCookie.position;
        poidInsulineStartPos = poidInsuline.position;
    }

    void Update()
    {
        int difference = GameController.instance.DiskDifference();
        float targetAngle = Mathf.Clamp(difference * anglePerDisk, -maxAngle, maxAngle);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -targetAngle);
        bar.transform.rotation = Quaternion.Lerp(bar.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Déplacement vertical des poids
        float yOffset = targetAngle * poidYOffsetMultiplier;

        // Le poidCookie monte quand l’angle est négatif (barre penche vers insuline)
        Vector3 cookieTargetPos = poidCookieStartPos + new Vector3(0f, yOffset, 0f);
        Vector3 insulinTargetPos = poidInsulineStartPos - new Vector3(0f, yOffset, 0f);

        poidCookie.position = Vector3.Lerp(poidCookie.position, cookieTargetPos, Time.deltaTime * rotationSpeed);
        poidInsuline.position = Vector3.Lerp(poidInsuline.position, insulinTargetPos, Time.deltaTime * rotationSpeed);
    }
}

