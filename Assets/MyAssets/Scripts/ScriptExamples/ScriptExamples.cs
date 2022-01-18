using UnityEngine;
using UnityEngine.AI;

public class ScriptExamples : MonoBehaviour
{
    // Source: https://youtu.be/Zmh1FFywwx4 - Cómo mover un personaje en Unity | 4 Métodos
    // Move a Character using Transform. No collisions, no gravity
    // If collision required, box collider and rb can be assigned, gravity can be enabled
    // rb rotation may be frozen in the editor to avoid rotation after colliding with other game objects
    // collision will generate a bouncing behaviour
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 moveInput;

    private void Update()
    {
        moveInput = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) moveInput.z = 1;
        if (Input.GetKeyDown(KeyCode.S)) moveInput.z = -1;
        if (Input.GetKeyDown(KeyCode.D)) moveInput.x = 1;
        if (Input.GetKeyDown(KeyCode.A)) moveInput.z = -1;
        
        Move(moveInput);
    }

    private void Move(Vector3 directionArg)
    {
        transform.position += directionArg.normalized * speed * Time.deltaTime;
    }
    
    // -------------------------------------------------------------------------
    
    // Using rb and physics as fall guys. It avoids bouncing
    // Acceleration ignores the game object's mass
    // rb rotation can be frozen
    
    private void GetRigidbody()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveWithPhysics(moveInput);
    }

    private void MoveWithPhysics(Vector3 directionArg)
    {
        // move the game object with force, acceleration
        // rb.AddForce(directionArg.normalized * speed, ForceMode.Acceleration);
        
        // Move as Transform, but with physics, no force, instant move
        // Avoid modifying velocity directly because it can result in unrealistic behaviour
        // instead, use AddForce (Unity documentation)
        rb.MovePosition(rb.position + directionArg.normalized* speed * Time.fixedDeltaTime); 
    }

    // ---------------------------------------------------------------------------------
    
    // *** Most used, avoid physics but keep collisions working *** Character controller
    // slope (pendiente) and maximum step height with 
    // Slope limit and step offset - can be modified in Editor
    // if character needs to push physics objects, it requires collider and rb
    // rb must be set as kinematic to let the engine know the game object is not being moved by physics
    // and that we want the physics (from other objects) don't affect it

    [SerializeField] private CharacterController characterController;

    private void GetCharacterController()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    // Use in Update
    private void MoveWithCharacterController(Vector3 directionArg)
    {
        characterController.SimpleMove(directionArg.normalized * speed);
    }
    
    // ---------------------------------------------------------------------------------
    
    // *** NavMesh Agent: move the character to a position dodging (esquivando) obstacles
    // To create a nav mesh, mark the game objects such as ground and stairs as static
    // In the navigation tab, bake tab, bake

    [SerializeField] private NavMeshAgent navMeshAgent;

    private void GetNavMeshAgent()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)) MoveWithNavMesh(hit.point); 
        }
    }

    private void MoveWithNavMesh(Vector3 positionArg)
    {
        navMeshAgent.SetDestination(positionArg);
    }
}
