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
    Vector3 velocity;
    float stepTime = 0f;
    Transform aimLaser;
    Transform laserStart;
    AudioSource step;
    AudioSource cry;
    AudioSource electrify;
    AudioSource gunLoad;
    AudioSource gunFire;

	// Use this for initialization
	void Start () {
		speed = maxSpeed;
		controller = gameObject.GetComponent<CharacterController>();
		animator = gameObject.GetComponent<Animator>();
		gun = transform.Find("CATRigPelvis/CATRigSpine1/CATRigSpine2/CATRigTorso/CATRigRArmCollarbone/CATRigRArm1/CATRigRArm2/CATRigRArmPalm/gun");
        laserStart = gun.Find("Muzzle");
        aimLaser = gun.Find("AimLaser");
		gun.localScale = Vector3.zero;

        AudioSource[] audios = GetComponents<AudioSource>();
        step = audios[0];
        cry = audios[1];
        electrify = audios[2];
        gunLoad = audios[3];
        gunFire = audios[4];
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
                    if (!gunLoad.isPlaying)
                    {
                        gunLoad.Play();
                    }
					Plane playerFeet = new Plane(Vector3.up, transform.position);
					Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
					float enter = 0f;
					if(playerFeet.Raycast(aimRay, out enter)){
						aimTarget = aimRay.GetPoint(enter);
						transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, aimTarget-transform.position, 0.5f, 0f));
					}
                    RaycastHit hit;
                    Vector3 rayDirection = (aimTarget - laserStart.position);
                    rayDirection.y = 0f;
                    if (Physics.Raycast(laserStart.position, laserStart.forward, out hit))
                    {
                        
                        Quaternion rotForwardToUp = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
                        Vector3 difference = hit.point - laserStart.position;

                       
                        aimLaser.position = Vector3.Lerp(laserStart.position, hit.point, 0.5f);
                        aimLaser.rotation = Quaternion.LookRotation(difference)*rotForwardToUp;
                        aimLaser.localScale = new Vector3(0.2f, hit.distance, 0.2f)*(1/gun.localScale.x);
                        Debug.DrawLine(laserStart.position, hit.point, Color.magenta);
                    }

				}else{
                   // Destroy(aimLaser);
                    gunLoad.Stop();
					transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, input, 0.5f, 0f));
				}
			}
		}

		if(movementAllowed) controller.Move(input*Time.deltaTime*speed);
		animator.SetFloat("Velocity", controller.velocity.magnitude);

        float forward = Vector3.Project(velocity * 15, transform.forward).magnitude * (Vector3.Angle(velocity, transform.forward) > 90 ? -1 : 1) * 2;
        float right = Vector3.Project(velocity * 15, transform.right).magnitude * (Vector3.Angle(velocity, transform.right) > 90 ? -1 : 1) * 2;

        animator.SetFloat("forward", forward);
        animator.SetFloat("right", right);
		
		controller.Move(Vector3.down*9.81f*Time.deltaTime);
        velocity = (transform.position - startPos);
	}

    public void OnGUI()
    {


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
            gunCharge = Mathf.Clamp(gunCharge, 0, 1);
			gun.localScale = Vector3.Lerp(gun.localScale, Vector3.one*1.5f, 0.3f);
            gunLoad.pitch = 1 + gunCharge;
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
        gunFire.Play();
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
        return velocity.magnitude != 0f;
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
            electrify.Play();
            Invoke("NoiseCry", 1f);
			dead = true;
			animator.SetTrigger("ShockDie");
		}
	}
	
	public bool IsDead(){
		return dead;
	}

    public void Step()
    {
        if (Time.time - stepTime > 0.1f)
        {
            GetComponent<AudioSource>().Play();
            stepTime = Time.time;
        } 
    }

    public void NoiseCry()
    {
        cry.Play();
    }
}
