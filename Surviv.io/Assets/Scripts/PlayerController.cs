using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space(5f), Header("Player Stats"), SerializeField]
    public float moveSpeed = 5.0f;
    [Space(5f), Header("Player Physics"), SerializeField]
    private Rigidbody2D rb;
    [SerializeField] private Transform _bulletSpawnPoint;
    [Space(5f), Header("Camera"), SerializeField] private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        _camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _camera.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(Resources.Load("Bullet")) as GameObject;
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = _bulletSpawnPoint.rotation;
        Destroy(bullet, 5.0f);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    private void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
