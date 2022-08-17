using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCursor : MonoBehaviour
{
    RectTransform recTrans;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 cursorPos;
    [SerializeField] private Vector3 offSett;
    [SerializeField] private List<Sprite> Sprites;
    private Image image;
    private int currentIndex = 0;
    private bool isCooling = false;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        recTrans = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.sprite = Sprites[0];
    }

    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 100;
        recTrans.position = Vector3.Lerp(recTrans.position, cam.ScreenToWorldPoint(pos) + offSett, Time.deltaTime);
        Switch();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        isCooling = false;
    }

    private void Switch()
    {
        if(Input.GetKey(KeyCode.Mouse2) && !isCooling)
        {
            isCooling = true;
            StartCoroutine(Cooldown());
            if(currentIndex < Sprites.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            image.sprite = Sprites[currentIndex];
        }
    }
}
