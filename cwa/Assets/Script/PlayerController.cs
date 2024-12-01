using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {
  // Start is called before the first frame update
  const float gravityValue = -9.8f;
  Rigidbody rb;
  [SerializeField]
  float InputX;
  [SerializeField]
  float InputZ;
  [SerializeField]
  bool isFalling;
  bool isJumped;
  bool isGround;
  CapsuleCollider capsuleCollider;
  [SerializeField]
  float speed = 10f;
  [SerializeField]
  Vector3 velocity;

  void Start() {
    rb = GetComponent<Rigidbody>();
    rb.isKinematic = true;
    capsuleCollider = GetComponent<CapsuleCollider>();
  }

  // Update is called once per frame
  void Update() {
    isFalling = !CheckGrounded(out RaycastHit groundHit);
    InputMovement();
    HandleGravity();
    Vector3 input = new Vector3(InputX, 0, InputZ).normalized;
    Vector3 movement = new Vector3(input.x, 0, input.z);
    transform.position = MovePlayer(movement * speed * Time.deltaTime);
    transform.position = MovePlayer(velocity * Time.deltaTime);
  }

  void HandleGravity() {
    if (isFalling) {
      velocity.y += (gravityValue * Time.deltaTime);
    } else {
      velocity.y = 0.0f;
    }
  }

  Vector3 MovePlayer(Vector3 movement) {
    Vector3 positon = transform.position;
    positon += movement;
    return positon;
  }
  private bool CheckGrounded(out RaycastHit groundHit) {
    bool onGround = CastSelf(transform.position, transform.rotation,
                             Vector3.down, 0.01f, out groundHit);
    float angle = Vector3.Angle(groundHit.normal, Vector3.up);
    return onGround;
  }

  void InputMovement() {
    InputX = Input.GetAxis("Horizontal");
    InputZ = Input.GetAxis("Vertical");
  }

  public bool CastSelf(Vector3 pos, Quaternion rot, Vector3 dir, float dist,
                       out RaycastHit hit) {
    // Get Parameters associated with the KCC
    Vector3 center = rot * capsuleCollider.center + pos;
    float radius = capsuleCollider.radius;
    float height = capsuleCollider.height;

    // Get top and bottom points of collider
    Vector3 bottom = center + rot * Vector3.down * (height / 2 - radius);
    Vector3 top = center + rot * Vector3.up * (height / 2 - radius);

    // Check what objects this collider will hit when cast with this
    // configuration excluding itself
    IEnumerable<RaycastHit> hits =
        Physics
            .CapsuleCastAll(top, bottom, radius, dir, dist, ~0,
                            QueryTriggerInteraction.Ignore)
            .Where(hit => hit.collider.transform != transform);
    bool didHit = hits.Count() > 0;

    // Find the closest objects hit
    float closestDist =
        didHit ? Enumerable.Min(hits.Select(hit => hit.distance)) : 0;
    IEnumerable<RaycastHit> closestHit =
        hits.Where(hit => hit.distance == closestDist);

    // Get the first hit object out of the things the player collides with
    hit = closestHit.FirstOrDefault();
    // Return if any objects were hit
    return didHit;
  }
}
