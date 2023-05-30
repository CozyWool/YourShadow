using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
public class DamagePopup : MonoBehaviour
{
   
    public static DamagePopup Create(Vector3 position, int damageAmount,bool isCrit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount,isCrit);
        return damagePopup;
    }

    private static int sortingOrder;

    private TextMeshPro textMesh;
    private float destroyTimer = 0.3f;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, bool isCriticalHit)
    {
        if (!isCriticalHit)
        {
            textMesh.fontSize = 36;
            textColor = UtilsClass.GetColorFromString("FFFFFF");
        }
        else
        {
            textMesh.fontSize = 45;
            textColor = UtilsClass.GetColorFromString("FFF500");
        }
        textMesh.color = textColor;
        textMesh.SetText(damageAmount.ToString());
        destroyTimer = 0.3f;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        float moveY = 20f;
        transform.position += new Vector3(0, moveY) * Time.deltaTime;

        if (destroyTimer > 0.3f * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0)
        {
            float destroySpeed = 6f;
            textMesh.alpha -= destroySpeed * Time.deltaTime;
            if (textMesh.alpha < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
