using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Perfect : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float destroyDelay = 1f;

    [SerializeField] TextMesh pointsText;
    [SerializeField] TextMesh pointsTextShadow;
    
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
    }

    public void SetComboMultiplier(int multiplier) {
        if (multiplier > 1) {
            string text = "+50x" + multiplier;

            pointsText.text = text;
            pointsTextShadow.text = text;
        }
    }
}
