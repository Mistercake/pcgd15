using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 3f;
	float speed;
	
	CharacterController controller;
	Animator animator;
	bool preventMovement = false;
	Vector3 outsideInput = Vector3.zero;
	bool useOutsideInput = false;
	Vector3 faceTarget = Vector3.zero;
	bool useFaceTarget = false;
	bool aiming = false;
	float gunCharge = 0f;
	Transform gun;
	Vector3 aimTarget;

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
		
		UpdateGun();
		UpdateMovement();
		
		
	}
	
	void UpdateMovement(){
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
		
		if(!preventMovement) controller.Move(input*Time.deltaTime*speed);
		animator.SetFloat("Velocity", controller.velocity.magnitude);
		
		controller.Move(Vector3.down*9.81f*Time.deltaTime);
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
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base.Aiming")){
			preventMovement = true;
		}else{
			preventMovement = false;
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
		Transform muzzle = gun.Find("Muzzle");
		Vector3 gunTarget = aimTarget;
		gunTarget.y = muzzle.position.y;
		animator.SetTrigger("Shoot");
		gunCharge = 0f;
		
		RaycastHit hit;
		
		if (Physics.Raycast(muzzle.position, (gunTarget-muzzle.position), out hit, Mathf.Infinity, (1 << 9))){ // Raycast only for the layer 'Enemies'
			Debug.Log("Hit a guard.");
			hit.transform.GetComponent<GuardMovement>().Die();
		}
	}
	
	public void SetMovement(bool val){
		preventMovement = val;
	}
	
	public void SetInput(Vector3 input){
		input.y = 0f;
		outsideInput = input;
		useOutsideInput = true;
	}
	
	public void SetFaceTarget(Vector3 target){
		target.y = 0f;
		faceTarget = target;
		useFaceTarget = true;
	}
	
	public void SetHacking(bool value){
		animator.SetBool("Hacking", value);
	}
}
