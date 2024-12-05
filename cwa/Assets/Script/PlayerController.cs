using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
  // Start is called before the first frame update
  float minGravity = -0.05f;
  float gravity = -10f;
  float _maxJumpHeight = 3f;
  float _maxJumpTime = 0.1f;

  [SerializeField]
  float InputX;
  [SerializeField]
  float InputZ;


  Vector3 _cameraRelativeMovement;
  public ParticleSystem _moveParticle;

  [SerializeField] bool isJumping;
  [SerializeField] bool isGround;
  [SerializeField] bool isJumpedPress;
  [SerializeField] bool isMovePressed;


  CapsuleCollider capsuleCollider;

  [SerializeField]
  float speed = 10f;
  float initialJumpVelocity;

  [SerializeField]
  Vector3 velocity;
  public LayerMask layerMask;
  Bounds bounds;
  int maxBounces = 5;
  float skinWidth = 0.015f;
  int isRuningHash;
  int isJumpingHash;
  Animator anim;
  float counter;

  void Awake () {
    SetUpJumpVariable();
  }
  void Start() {
    anim = GetComponentInChildren<Animator>();
    capsuleCollider = GetComponent<CapsuleCollider>();
    isRuningHash = Animator.StringToHash("Running");
    isJumpingHash = Animator.StringToHash("Jumping");

  }


  // Update is called once per frame
  void Update() {
    isGround = CheckGrounded(out RaycastHit groundHit);
    InputMovement();
    Vector3 input = new Vector3(InputX, 0, InputZ).normalized;
    _cameraRelativeMovement = ConvertToCameraSpace(input) * speed;
    velocity = new Vector3(_cameraRelativeMovement.x, velocity.y, _cameraRelativeMovement.z);
    transform.position += Movement(velocity * Time.deltaTime);
    HandleRotation();
    HandleGravity();
    HandleJump();
    handleAnimation();
    handleParticle();
  }

 void SetUpJumpVariable () {
     float timeToApex = _maxJumpTime / 2;
     //gravity = ( -2 * _maxJumpHeight)/ Mathf.Pow(timeToApex,2);
     initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;

 }
  void HandleGravity() {
     bool isFalling = velocity.y  < 0 || !isJumpedPress;
     if(isGround){
        velocity.y = 0;
     }
     else if (isFalling)
     {
        float newYVelcoity = velocity.y + (gravity * Time.deltaTime) * 10f;
        float nextYVelcoity = (velocity.y + newYVelcoity) *.5f;
        velocity.y = nextYVelcoity;
     }
     else{
        float newYVelcoity = velocity.y + (gravity * Time.deltaTime);
        float nextYVelcoity = (velocity.y + newYVelcoity) *.5f;
        velocity.y = nextYVelcoity;
     }
  }

  float  maxSlopAngle = 55;
  // custome collision
  private Vector3 CollideAndSlide(Vector3 vel, Vector3 pos, int depth, bool gravityPass) {
    if (depth >= maxBounces) {
      return Vector3.zero;
    }
    float dist = vel.magnitude + skinWidth;
    float radius = capsuleCollider.radius;
    RaycastHit hit;
    Vector3 p1 = pos + capsuleCollider.center +
                 Vector3.up * (( -capsuleCollider.height * 0.5F)+radius);
    Vector3 p2 = p1 + Vector3.up * capsuleCollider.height;
    if (Physics.CapsuleCast(p1, p2, radius, vel.normalized, out hit, dist,
                            layerMask)) {
      Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);
      Vector3 leftOver = vel - snapToSurface;
      float angle = Vector3.Angle(Vector3.up, hit.normal);

      if (snapToSurface.magnitude <= skinWidth) {
        snapToSurface = Vector3.zero;
      }


      if(angle <= maxSlopAngle){
        if(gravityPass){
              return snapToSurface;
        }
        leftOver = ProjectAndScale(leftOver, hit.normal);
      }else{
      }
      return snapToSurface +
             CollideAndSlide(leftOver, pos + snapToSurface, depth + 1, gravityPass);
    }
    return vel;
  }

  private Vector3 ProjectAndScale(Vector3 vec, Vector3 normal){
      float mag = vec.magnitude;
      vec = Vector3.ProjectOnPlane(vec, normal).normalized;
      vec *= mag;
      return vec;
  }

  Vector3 ConvertToCameraSpace (Vector3 vector) {
      Vector3 cameraForward = Camera.main.transform.forward;
      Vector3 cameraRight = Camera.main.transform.right;
      cameraForward = cameraForward.normalized;
      cameraRight = cameraRight.normalized;
      Vector3 cameraForwardZProduct = vector.z * cameraForward;
      Vector3 cameraRightXProduct = vector.x * cameraRight;
      Vector3 vectorRotatedToCameraSpace = cameraRightXProduct + cameraForwardZProduct;
      return new Vector3(vectorRotatedToCameraSpace.x, vector.y, vectorRotatedToCameraSpace.z);
  }
  void HandleRotation () {
     if(InputX == 0 && InputZ ==0) {
        return;
     }
     Vector3 vectorToLookAt;
     vectorToLookAt.x = _cameraRelativeMovement.x;
     vectorToLookAt.y = 0;
     vectorToLookAt.z = _cameraRelativeMovement.z;

     Quaternion currentRotation = transform.rotation;
     transform.rotation = Quaternion.Slerp(currentRotation, Quaternion.LookRotation(vectorToLookAt), 50f * Time.deltaTime);
  }

  Vector3 Movement(Vector3 velocity) {
    Vector3 positon = transform.position;
    Vector3 movement = CollideAndSlide(velocity, positon, 0,false);
    if(!isJumping){
        movement += CollideAndSlide(Vector3.up * gravity * Time.deltaTime, positon + movement, 0,true);
    }
    return movement;
  }


  private bool CheckGrounded(out RaycastHit groundHit) {
    Vector3 p1 = transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height/2 - capsuleCollider.radius);
    Vector3 p2 = p1 +  Vector3.up  * capsuleCollider.height;
    return Physics.CapsuleCast( p1,p2 ,capsuleCollider.radius, Vector3.down, out groundHit,0.05f,
                              layerMask);
  }

  private void HandleJump() {
     if(isGround && isJumpedPress) {
        isJumping = true;
        velocity.y = 10f;
     } else if (!isJumpedPress && isJumping && isGround ){
        isJumping = false;
     }
  }


  private void handleAnimation() {
    bool isRunning = anim.GetBool(isRuningHash);
    bool isJumpingAni = anim.GetBool(isJumpingHash);
        Debug.Log(isRunning);
    if(isMovePressed  && !isRunning) {
        anim.SetBool(isRuningHash, true);
    }
    if(!isMovePressed && isRunning) {
        anim.SetBool(isRuningHash, false);
    }

    if(isJumpedPress  && !isJumpingAni && isJumping) {
        anim.SetBool(isJumpingHash, true);
    }

    if(!isJumpedPress  && isJumpingAni && !isJumping) {
        anim.SetBool(isJumpingHash, true);
    }
  }
  void handleParticle() {
    counter += Time.deltaTime;
   if(isMovePressed && !isJumping){
     if(counter > 0.05){
        _moveParticle.Play();
        counter = 0;
     }
   }
  }

  void Jump() {
    isJumpedPress = true;
  }
  void InputMovement() {
    InputX = Input.GetAxis("Horizontal");
    InputZ = Input.GetAxis("Vertical");

    float Speed = new Vector2(InputX, InputZ).sqrMagnitude;


    isMovePressed  = Speed > 0;
    if(Input.GetKeyDown("space")){
        Jump();
        Debug.Log("jump");
    }
    if(Input.GetKeyUp("space")){
        isJumpedPress = false;
    }
  }
}
