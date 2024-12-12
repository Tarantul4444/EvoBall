using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private GameMenuManager gameMenuManager;
    [SerializeField] private HeartsController heartsController;
    [SerializeField] private Material[] materials;

    private float startTime;
    private const float timeLimit = 100f;
    private int lives = 3;
    private Rigidbody playerRigidBody;
    private bool isGrounded = true;
    private bool isTouchingObstacle = false;
    private bool isLava = false;
    private Renderer playerRenderer;
    private Transform playerTransform;

    private string starsLevel;
    private char[] starsData;

    void Start() {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;

        startTime = Time.time;

        starsData = PlayerPrefs.GetString(starsLevel, "000").ToCharArray();
        starsLevel = $"Stars{SceneManager.GetActiveScene().name}";

        playerRenderer = GetComponentInChildren<Renderer>();
        playerTransform = transform.GetChild(0);
    }

    void Update() {
        float horizontalPosition = Input.GetAxis("Horizontal");
        float verticalPosition = Input.GetAxis("Vertical");

        if (isTouchingObstacle) horizontalPosition = 0f;

        Vector3 playerDirection = new Vector3(horizontalPosition, 0f, verticalPosition);
        playerRigidBody.velocity = new Vector3(playerDirection.x * _speed, playerRigidBody.velocity.y, playerDirection.z * _speed);

        if (playerDirection.magnitude > 0.1f) playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isLava) {
            playerRigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
        if (collision.gameObject.CompareTag("Obstacle")) {
            if (isLava) Destroy(collision.gameObject);
            else isTouchingObstacle = true;
        }
        if (collision.gameObject.CompareTag("GroundLava")) changeMaterial(1);

        if (collision.gameObject.CompareTag("Finish")) {
            starsData[0] = '1';
            if (lives == 3 || starsData[1] == '1') {
                starsData[1] = '1';
            }
            if (Time.time - startTime <= timeLimit || starsData[2] == '1') {
                starsData[2] = '1';
            }

            PlayerPrefs.SetString(starsLevel, new string(starsData));
            PlayerPrefs.Save();

            gameMenuManager.PauseLevel();
            gameMenuManager.OpenWinPanel(new string(starsData));
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) isTouchingObstacle = false;
    }

    public void getDamage() {
        if (lives > 0) --lives;
        else {
            gameMenuManager.PauseLevel();
            gameMenuManager.OpenWinPanel("000");
        }
        heartsController.RemoveHeart();
    }

    private void changeMaterial(int materialIndex) {
        isLava = materialIndex == 1 ? true : false;
        playerRenderer.material = materials[materialIndex];
    }
}