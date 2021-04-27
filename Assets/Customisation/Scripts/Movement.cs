using UnityEngine;

namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        // Make all the variables.
        #region Variables.
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        private CharacterController _charC;
        private Animator myAnimator;
        #endregion

        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            myAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_charC.isGrounded)
            {
                if (Input.GetButton("Crouch"))
                {
                    moveSpeed = runSpeed;
                    myAnimator.SetFloat("speed", 0.25f);
                }
                else
                {
                    if (Input.GetButton("Sprint"))
                    {
                        moveSpeed = crouchSpeed;
                        myAnimator.SetFloat("speed", 3f);
                    }
                    else if (!Input.GetButton("Sprint"))
                    {
                        moveSpeed = walkSpeed;

                        myAnimator.SetFloat("speed", 1f);
                    }
                }

                myAnimator.SetBool("walking", movementInput.magnitude > 0.05f);

                _moveDir = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * moveSpeed);
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpSpeed;
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }

    }
}