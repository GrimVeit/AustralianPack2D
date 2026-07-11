using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFly : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private RectTransform area;


    [Header("UI Elements")]
    [SerializeField] private List<Image> elements = new();


    [Header("Sprites Pool")]
    [SerializeField] private List<Sprite> sprites = new();



    [Header("Physics")]
    [SerializeField] private float minSpeed = 200f;
    [SerializeField] private float maxSpeed = 600f;

    [SerializeField] private float minRotation = -180f;
    [SerializeField] private float maxRotation = 180f;

    [SerializeField] private float bouncePower = 0.85f;



    private class ElementData
    {
        public Image image;
        public RectTransform rect;

        public Vector2 velocity;
        public float rotationSpeed;

        public Vector2 halfSize;
    }


    private readonly List<ElementData> data = new();


    private bool isActive;



    private void Awake()
    {
        Prepare();
    }



    private void Prepare()
    {
        data.Clear();


        foreach (var image in elements)
        {
            if (image == null)
                continue;


            ElementData item = new();

            item.image = image;
            item.rect = image.rectTransform;

            item.halfSize =
                image.rectTransform.rect.size / 2f;


            data.Add(item);
        }
    }



    private void Update()
    {
        if (!isActive)
            return;


        float dt = Mathf.Min(
            Time.unscaledDeltaTime,
            0.033f
        );


        foreach (var item in data)
        {
            item.rect.anchoredPosition +=
                item.velocity * dt;


            item.rect.Rotate(
                Vector3.forward,
                item.rotationSpeed * dt
            );


            CheckBounds(item);
        }
    }



    private void CheckBounds(ElementData item)
    {
        Vector2 pos =
            item.rect.anchoredPosition;



        float halfW =
            area.rect.width / 2f;

        float halfH =
            area.rect.height / 2f;



        float minX = -halfW + item.halfSize.x;
        float maxX = halfW - item.halfSize.x;

        float minY = -halfH + item.halfSize.y;
        float maxY = halfH - item.halfSize.y;



        if (pos.x > maxX)
        {
            pos.x = maxX;
            item.velocity.x *= -bouncePower;
        }


        if (pos.x < minX)
        {
            pos.x = minX;
            item.velocity.x *= -bouncePower;
        }


        if (pos.y > maxY)
        {
            pos.y = maxY;
            item.velocity.y *= -bouncePower;
        }


        if (pos.y < minY)
        {
            pos.y = minY;
            item.velocity.y *= -bouncePower;
        }


        item.rect.anchoredPosition = pos;
    }




    public void EnableAnimation()
    {
        isActive = true;


        foreach (var item in data)
        {
            // назначаем новый спрайт
            if (sprites.Count > 0)
            {
                item.image.sprite =
                    sprites[
                        Random.Range(0, sprites.Count)
                    ];
            }


            float angle =
                Random.Range(0f, 360f);


            item.velocity =
                new Vector2(
                    Mathf.Cos(angle),
                    Mathf.Sin(angle)
                )
                *
                Random.Range(minSpeed, maxSpeed);



            item.rotationSpeed =
                Random.Range(
                    minRotation,
                    maxRotation
                );
        }
    }



    public void DisableAnimation()
    {
        isActive = false;


        foreach (var item in data)
        {
            item.velocity = Vector2.zero;
            item.rotationSpeed = 0;
        }
    }
}
