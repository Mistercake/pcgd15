using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 3f;
	float speed;
	
	CharacterController controller;
	Animator animator;
	bool movementAllowed = true;
	Vector3 outsideInput = Vector3.zero;
	bool useOutsideInput = false;
	Vector3 faceTarget = Vector3.zero;
	bool useFaceTarget = false;
	bool aiming = false;
	float gunCharge = 0f;
	Transform gun;
	Vector3 aimTarget;
	bool dead = false;
    float velocity;

	// Use this for initialization
	void Start () {
		speed = maxSpeed;
		controller = gameObject.GetComponent<CharacterController>();
		animator = gameObject.GetComponent<Animator>();
		gun = transform.Find("CATRigPelvis/CATRigSpine1/CATRigSpine2/CATRigTorso/CATRigRArmCollarbone/CATRigRArm1/CATRigRArm2/CATRigRArmPalm/gun");
		gun.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead){
			UpdateGun();
			UpdateMovement();
		}
	}
	
	void UpdateMovement(){
        Vector3 startPos = transform.position;
	
		Camera camera = Camera.main;
		Vector3 cameraForward = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up).normalized;
		Vector3 cameraRight = Vector3.ProjectOnPlane(camera.transform.right, Vector3.up).normalized;
		Vector3 input = cameraForward*Input.GetAxis("Vertical") + cameraRight*Input.GetAxis("Horizontal");
		if(useOutsideInput){
			input = outsideInput;
			useOutsideInput = false;
		}
		
		if(Vector3.Angle(transform.forward, input) > 0.1f){
			if(useFaceTarget){
				transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, faceTarget-transform.position, 0.5f, 0f));
				useFaceTarget = false;
			}else{
				if(aiming){
					Plane playerFeet = new Plane(Vector3.up, transform.position);
					Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
					float enter = 0f;
					if(playerFeet.Raycast(aimRay, out enter)){
						aimTarget = aimRay.GetPoint(enter);
						transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, aimTarget-transform.position, 0.5f, 0f));
					}
				}else{
					transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, input, 0.5f, 0f));
				}
			}
		}

		if(movementAllowed) controller.Move(input*Time.deltaTime*speed);
		animator.SetFloat("Velocity", controller.velocity.magnitude);
		
		controller.Move(Vector3.down*9.81f*Time.deltaTime);
        velocity = (transform.position - startPos).magnitude;
	}
	
	void UpdateGun(){
		if(Input.GetButtonDown("Aim")){
			aiming = true;
			animator.SetBool("Aiming", aiming);
			gun.Find("loading").particleSystem.Play();
		}
		if(Input.GetButtonUp("Aim")){
			aiming = false;
			animator.SetBool("Aiming", aiming);
			gun.Find("loading").particleSystem.Stop();
		}

		
		if(aiming){
			speed = 1.5f;
			gunCharge += 0.5f*Time.deltaTime;
			gun.localScale = Vector3.Lerp(gun.localScale, Vector3.one*1.5f, 0.3f);
		}else{
			speed = maxSpeed;
			gunCharge = 0f;
			gun.localScale = Vector3.Lerp(gun.localScale, Vector3.zero, 0.3f);
		}
		
		if(gunCharge >= 1 && gun.Find("ready").particleSystem.isStopped){
			gun.Find("ready").particleSystem.Play();
		}
		if(gunCharge < 1 && gun.Find("ready").particleSystem.isPlaying){
			gun.Find("ready").particleSystem.Stop();
		}
		
		if(Input.GetButtonDown("Fire1") && gunCharge >= 1){
			Shoot();
		}
	}
	
	void Shoot(){
		gun.Find("fire").particleSystem.Play();
		Vector3 muzzle = transform.position+Vector3.up*0.5f;
		Vector3 gunTarget = aimTarget;
	//	gunTarget.y = transform.TransformPoint(muzzle.position).y;
		animator.SetTrigger("Shoot");
		gunCharge = 0f;
		
		RaycastHit hit;
		
		if (Physics.Raycast(muzzle, (gunTarget-transform.position), out hit, Mathf.Infinity/*, ~(1<<10)*/)){ // all layers except 'Player' (layer 10)
			Debug.Log(hit.transform);
			if(hit.transform.tag == "Guard"){
				Debug.Log("Hit a guard.");
				hit.transform.GetComponent<GuardMovement>().Die();
			}
		}
		Debug.DrawRay(muzzle, (gunTarget-transform.position), Color.red, 5f);
		
		foreach(Collider collider in Physics.OverlapSphere(gun.position, 10f, (1 << 9))){
			collider.gameObject.GetComponent<GuardVision>().Suspicion(gun.position);
		}
	}
	
	public void SetMovement(bool value){
		Debug.Log("SetMovement");
		this.movementAllowed = value;
	}
	
	public void SetInput(Vector3 input){
		input.y = 0f;
		outsideInput = input;
		useOutsideInput = true;
	}

    public bool IsMoving()
    {
        return velocity != 0f;
    }
	
	public void SetFaceTarget(Vector3 target){
		target.y = 0f;
		faceTarget = target;
		useFaceTarget = true;
	}
	
	public void SetHacking(bool value){
		animator.SetBool("Hacking", value);
	}
	
	public void Die(){
		if(!dead){
			dead = true;
			animator.SetTrigger("ShockDie");
		}
	}
	
	public bool IsDead(){
		return dead;
	}
}
