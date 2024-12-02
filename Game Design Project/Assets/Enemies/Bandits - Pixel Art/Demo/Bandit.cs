using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    [SerializeField] RuntimeAnimatorController [] animations;


    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private CapsuleCollider2D   m_capsule2d;
    private Sensor_Bandit       m_groundSensor;
    private Enemy_Sensor_Bandit m_enemySensor;
    private AttackRadius        m_attackRadius;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    private int                 state = 0;
    private int                 scale = 1;
    private bool                moving = false;
    public int stamina = 1000;
    private bool running = false;
    private float run_speed = 1.25f;
    public int max_stamina = 1000;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_capsule2d = GetComponent<CapsuleCollider2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_enemySensor = transform.Find("EnemySensor").GetComponent<Enemy_Sensor_Bandit>();
        m_attackRadius = transform.Find("AttackRadius").GetComponent<AttackRadius>();
    }
	
	// Update is called once per frame
	void Update () {

        //ensures player is not pushing enemies, but can run away from them
        //and can play proper animations
        moving = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-3.0f, 3.0f, 3.0f);

        if(Input.GetKey(KeyCode.LeftShift)){
            if(inputX != 0){
                if(stamina <= 0){
                    running = false;
                }
                else if(stamina > 0){
                    stamina -= 1;
                    running = true;
                }
            }
        }else{
            if(inputX == 0){
                running = false;
                if(stamina >= max_stamina){
                    stamina = max_stamina;
                }
                else if(stamina < max_stamina){
                    stamina += 1;
                }
                if(stamina < 0){
                    stamina = 0;
                }
            }
        }
        
        // Move if not running into enemy
        if (!m_enemySensor.StateLeft() && !m_enemySensor.StateRight())
        {
            moving = true;
            m_body2d.velocity = new Vector2(inputX * m_speed * (running ? run_speed:1), m_body2d.velocity.y);
        }
        else if (m_enemySensor.StateLeft())
        {
            if (inputX > 0)
            {
                m_body2d.velocity = new Vector2(inputX * m_speed * (running ? run_speed:1), m_body2d.velocity.y);
                moving = true;
            }
        }
        else
        {
            if (inputX < 0)
            {
                m_body2d.velocity = new Vector2(inputX * m_speed * (running ? run_speed:1), m_body2d.velocity.y);
                moving = true;
            }
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
            {
                m_animator.SetTrigger("Death");
            }
            else
            {
                m_animator.SetTrigger("Recover");
            }

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
        {
            m_animator.SetTrigger("Hurt");
        }

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
            Invoke("Attack", 0.2f);
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_animator.SetTrigger("Jump");
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.4f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && moving)
        {
            m_animator.SetInteger("AnimState", 2);
        }

        //Combat Idle
        else if (m_combatIdle)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        //Idle
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }
        
        
    }

    void Attack()
    {
        // Activate the attack collider (trigger hit detection)
        m_attackRadius.activate();

        // Optionally, deactivate it after a short delay (to prevent continuous hits)
        Invoke("DeactivateAttack", 0.5f); // 0.1 seconds for the duration of the attack hitbox
    }

    void DeactivateAttack()
    {
        // Deactivate the attack collider after the attack duration
        m_attackRadius.deactivate();
    }

    public void SwapState(){
        if(state < animations.Length - 1){
            state++;
        }
        else{
            state = 0;
        }
        
        m_animator.runtimeAnimatorController = animations[state];
        m_groundSensor.Swap();


        switch(state){
           case 0:
               m_speed = 8.5f;
               m_jumpForce = 10f;
               break;
           case 1:
               m_speed = 6.5f;
               m_jumpForce = 8;
               m_capsule2d.size = new Vector2(.35f, .4f);
               m_capsule2d.offset = new Vector2(0f, .067f);
               m_groundSensor.SetOffset(0f, -.14f);
               break;
        }
    }
}
