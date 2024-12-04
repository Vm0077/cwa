using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
  // Start is called before the first frame update
  public float gravityValue = -9.8f;
  float minGravity = -0f;
  float gravity  = -9.8f;
  float _maxJumpHeight = 5f;
  float _maxJumpTime = 3f;

  [SerializeField]
  float InputX;
  [SerializeField]
  float InputZ;


  Vector3 _cameraRelativeMovement;

  [SerializeField]
  bool isFalling;
  bool isJumped;
  bool isGround;

  CapsuleCollider capsuleCollider;

  [SerializeField]
  float speed = 10f;

  [SerializeField]
  Vector3 velocity;
  public LayerMask layerMask;
  Bounds bounds;
  int maxBounces = 5;
  float skinWidth = 0.015f;
  Animator anim;

  void Start() {
    //anim = GetComponent<Animator>();
    capsuleCollider = GetComponent<CapsuleCollider>();
  }


  // Update is called once per frame
  void Update() {
    isFalling = !CheckGrounded(out RaycastHit groundHit);
    isJumped = !isFalling;
    InputMovement();
    HandleGravity();
    Vector3 input = new Vector3(InputX, 0, InputZ).normalized;
    _cameraRelativeMovement = ConvertToCameraSpace(input) * speed;
    velocity = new Vector3(_cameraRelativeMovement.x, velocity.y, _cameraRelativeMovement.z);
    transform.position += Movement(velocity * Time.deltaTime);
    HandleRotation();
  }


  void HandleGravity() {
     if(isFalling) {
        velocity.y += gravity  * Time.deltaTime;
     }else{
        velocity.y = minGravity;
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
    movement += CollideAndSlide(Vector3.up * gravity, positon + movement, 0,true);
    return movement;
  }


  private bool CheckGrounded(out RaycastHit groundHit) {
    return Physics.SphereCast( transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius), capsuleCollider.radius,
                              Vector3.down, out groundHit,0.05f,
                              layerMask);
  }

  private void handleJump() {
     isJumped = true;

  }
  void InputMovement() {
    InputX = Input.GetAxis("Horizontal");
    InputZ = Input.GetAxis("Vertical");

    float Speed = new Vector2(InputX, InputZ).sqrMagnitude;
    //float allowPlayerRotation = 5f;
//	if (Speed > allowPlayerRotation) {
//			anim.SetFloat ("Blend", Speed, 0.5f, Time.deltaTime);
//		} else if (Speed < allowPlayerRotation) {
//			anim.SetFloat ("Blend", Speed, 0.3f, Time.deltaTime);
//	}
    if(Input.GetKey("space")){
        handleJump();
        Debug.Log("jump");
    }
  }
}
