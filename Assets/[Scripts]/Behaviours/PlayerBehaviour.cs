////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 02:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file
//Description : script for manage player properties : moving, gain score, get pickups, firing, use ray
//              weapon
//              The initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public float speed = 2.0f;
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;

    public float playerSpeed = 10.0f;
    public bool usingMobileInput = false;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint2;

    public float fireRate = 0.2f;


    private Camera camera;
    //private ScoreManager scoreManager;
    private BulletManager bulletManager;
    private Vector2 screenWorldSize;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        //scoreManager = FindObjectOfType<ScoreManager>();

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        AdaptOrientations();

        if (usingMobileInput)
        {
            MobileInput();
        }
        else
        {
            ConventionalInput();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.K))
        {
            //scoreManager.AddPoints(10);
        }

    }

    private void AdaptOrientations()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        horizontalBoundary.max = screenWorldSize.x - 0.6f;
        horizontalBoundary.min = -screenWorldSize.x + 0.6f;
        verticalBoundary.max = 0.0f;
        verticalBoundary.min = -screenWorldSize.y + 0.5f;
        transform.localRotation = Quaternion.identity;

    }

    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * playerSpeed);
        }
    }

    public void ConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, 0.0f, 0.0f);

        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        transform.position += new Vector3(0.0f, y, 0.0f);
    }

    public void Move()
    {
        float clampedXPosition = Mathf.Clamp(transform.position.x, horizontalBoundary.min, horizontalBoundary.max);
        float clampedYPosition = Mathf.Clamp(transform.position.y, verticalBoundary.min, verticalBoundary.max);
        transform.position = new Vector2(clampedXPosition, clampedYPosition);
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
        var bullet2 = bulletManager.GetBullet(bulletSpawnPoint2.position, BulletType.PLAYER);

    }
}
