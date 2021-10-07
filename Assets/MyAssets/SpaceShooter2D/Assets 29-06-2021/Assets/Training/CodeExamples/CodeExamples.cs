/*
_______________ CODE EXAMPLES _______________

/*CODE EG: Increase, decrease speed **********

// if Key S, increase speed
if (Input.GetKeyDown(KeyCode.S)) speed += 5f;

// if Key A, decrease speed
if (Input.GetKeyDown(KeyCode.A)) speed -= 5f;

// if speed > 20, print out "Slow down"
// speed can't be lower than 0
if (speed > 20f) Log("Slow down");
else if (speed <= 0f)
{
   speed = 0f;
}

// if speed = 0, print out "Speed up"
if (speed == 0f) Log("Speed Up");

****************** ----- ***************

CODE EG: Change color if score > 50 ****************

[SerializeField] private int score;
[SerializeField] private WeaponLauncher playerGO;
[SerializeField] private bool isGreenColorSet;

playerGO.GetComponent<MeshRenderer>().material.color = Color.red;

if (Input.GetKeyDown(KeyCode.Space)) score += 10;

if (score > 50 && isGreenColorSet == false)
{
   playerGO.GetComponent<MeshRenderer>().material.color = Color.green;
   Log("Color changed to green");
   isGreenColorSet = true;
} 
****************** ----- ***************

CODE EG: Calculate grades average **********************

[SerializeField] private int points, grade1, grade2, grade3, grade4, grade5, gradesTotal;
[SerializeField] private float averageGrade;
[SerializeField] private bool sentMessage;

void Start()
{
   grade1 = RandomGrade();
   grade2 = RandomGrade();
   grade3 = RandomGrade();
   grade4 = RandomGrade();
   grade5 = RandomGrade();

   gradesTotal = grade1 + grade2 + grade3 + grade4 + grade5;

   averageGrade = Mathf.Round(gradesTotal / 4f * 100f) * 0.01f;
}

void Update()
{
   if (averageGrade > 90 && sentMessage == false)
   { Log("A"); sentMessage = true; }

   else if (averageGrade >= 80 && averageGrade < 90 && sentMessage == false)
   { Log("B"); sentMessage = true; }

   else if (averageGrade >= 70 && averageGrade < 80 && sentMessage == false)
   { Log("C"); sentMessage = true; }

   else if (sentMessage == false)
   { Log("F"); sentMessage = true; }
}

public int RandomGrade()
{
   int n = Random.Range(1, 100);
   return n;
}
****************** ----- ***************

CODE EG Move Player in restricted area **************

public void MovePlayer()
{
   /// translate the player with horiz and vert
   float horizontal = Input.GetAxis("Horizontal");
   float vertical = Input.GetAxis("Vertical");

   Vector2 moveDirection = new Vector2(horizontal, vertical, 0).normalized;

   transform.Translate(moveDirection * speed * Time.deltaTime);

*** Restriction 1 ***
   float xPos = transform.position.x;
   float yPos = transform.position.y;

   if (yPos >= 0f) transform.position = new Vector2(xPos, 0f, 0f);
   else if (yPos <= -3.5f) transform.position = new Vector2(xPos, -3.5f, 0f); 

   if (xPos >= 10.4f) transform.position = new Vector2(-10.4f, yPos, 0f);
   else if (xPos <= -10.4f) transform.position = new Vector2(10.4f, yPos, 0f); 

*** Restriction 2 ***

float xClamp = Mathf.Clamp(transform.position.x, -10f, 10f);
float yClamp = Mathf.Clamp(transform.position.y, -3f, 0f);
transform.position = new Vector2(xClamp, yClamp, 0);
} 
****************** ----- ***************

CODE EG Apply Damage *************

public float health = 30f;
void Update(){
    if (Input.GetKeyDown(KeyCode.Space) & !IsDead())
        Damage(Random.Range(1f, 4f)); }

public void Damage(float damage){
    health -= damage;
    if (IsDead()) { health = 0; Log("Player is dead"); } }
****************** ----- ***************

CODE EG Get All Players ***********
public WeaponLauncher[] players;

    private void Start() => players = GetPlayers();

    WeaponLauncher[] GetPlayers() {
        WeaponLauncher[] allPlayers = WeaponLauncher.FindGameObjectsWithTag("Player"); }
****************** ----- ***************

CODE EG Random Position ***********

public Vector3[] positions;
private int posIndex;

private void Start()
{
    posIndex = SetIndex();
    transform.position = SetPosition(posIndex);
    Log("Index: " + posIndex);
}

private int SetIndex() => Random.Range(1, 5);
private Vector3 SetPosition(int index)=> positions[index];
****************** ----- ***************

CODE EG Wizard spells ***********

public class Wizard : MonoBehaviour
{
    public Spell fireBlast;
    public int level = 1;
    public int exp;

    private void Start()
    {
        fireBlast = new Spell("Fire Blast", 3, 27, 34);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireBlast.Cast();
            exp += fireBlast.expGained;
        }
    }
}
-----------------------------------------

public class Wizard : MonoBehaviour
{
    public Spell[] spells;
   
}

public class Wizard : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public Spell[] spells;

    private void Update()
    {
        // Cast Ice blast only if in required level
        // Check if it has the proper Id
        // When hitting space, Cast only the spell in accordance to the level

        // Iterate through spells[] and compare to my current level
        // Cast spell
        // Modify level value in inspector for testing

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var spell in spells)
            {
                if (spell.levelRequired == level)
                {
                    spell.Cast();
                    // here particle effect can added
                    exp += spell.expGained;
                }
            }
        }
    }

}
****************** ----- ***************

CODE EG Creating Items Database *************

/// use [System.Serializable] to show variables in inspector
public class Item
{
    public string name, description;
    public int id;

    public Item() { }

    public Item(string name, int id, string description)
    {
        this.name = name; this.id = id; this.description = description;
    }
}

public class ItemDatabase : MonoBehaviour
{
    public Item sword,hammer,gun;

    private void Start()
    {
        sword = new Item("Sword", 1, "This is a sword");
        hammer = new Item("hammer",2,"This is a hammer");
    }
}
---------------------------------------------------------
public class ItemDatabase : MonoBehaviour
{
    public Item sword,hammer,gun;

    private void Start()
    {
        sword = CreateItem("Sword", 1, "This is a sword");;
    }

    private Item CreateItem(string name, int id, string description)
        {
            return new Item(name, id, description);
        }
}
----------------------------------------------------------

public class ItemDatabase : MonoBehaviour
{
    public Item[] items;
}

***************** --------------- **********************

CODE EG Weapons and Consumables from Item **************

using UnityEngine;
public class Item 
{
    // bread can be consumable
    // sword can be used for combat
    // Does it make sense to use one class for both? no
    
    public string name;
    public int itemID;
    public Sprite icon;
}
------------------------------
public class Weapon : Item
{
    public int attackBonus, prayerBonus, strengthBonus, MageBonus;
}
------------------------------
public class Consumable : Item
{
    public int healthAdded;
    public bool isPoison;
}


CODE EG Counting connected players ****************

public class Test : MonoBehaviour
{
    private void Start()
    {
        Players p1 = new Players();
        Players p2 = new Players();
        Players p3 = new Players();
        Players p4 = new Players();
        Players p5 = new Players();
        Players p6 = new Players();

        Log("Players count: " + Players.playersConnected);

    }
}
public class Players
{
    public int id;
    public string name;

    // This variable is shared among every Players instance
    public static int playersConnected;
    public Players()
    {
        playersConnected++;
    }
}
***************** --------------- **********************


CODE EG Enemies counter

public class Enemi : MonoBehaviour
{
    private ManagerUI uI;
    private void OnEnable()
    {
        Spawner.enemyCount++;
        uI = WeaponLauncher.Find("ManagerUI").GetComponent<ManagerUI>();
        uI.UpdateEnemyCount();
        Die();
    }

    private void OnDisable()
    {
        Spawner.enemyCount--;
        uI.UpdateEnemyCount();
    }

    void Die() => Destroy(gameObject, 2f);
}
----------------------------------------------

public class Spawner : MonoBehaviour
{
    public WeaponLauncher enemyPrefab;
    public static int enemyCount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(enemyPrefab);
        }
    }
}
----------------------------------------------

public class ManagerUI : MonoBehaviour
{
    public Text activeEnemiesText;

    public void UpdateEnemyCount() => 
        activeEnemiesText.text = "Active Enemies: " + Spawner.enemyCount;

}
***************** --------------- **********************


CODE EG Helper class

public class PlayerTest : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UtilityHelper.CreateCube(); 
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UtilityHelper.SetPositionToZero(this.gameObject);
        }
    }
***************** --------------- **********************

CODE EG Static constructor
Use it to call static constructor before instance members constructor

public class Test : MonoBehaviour
{
    private void Start()
    {
        Employee e1 = new Employee();
        var e2 = new Employee();
        var e3 = new Employee();
        var e4 = new Employee();
        var e5 = new Employee();
    }

    private void Update()
    {

    }
}
---------------------------------------------
public class Employee
{
    public int ID,salary;
    public string firstName;
    public static string company;

    public Employee() => Log("Instance members initialized");

    static Employee()
    {
        company = "Company Name";
        Log(company);
        Log("Initialized static members");
    }
}
***************** --------------- **********************

CODE EG Spawn GOs with spacee key and change color
public WeaponLauncher[] sourceObjects;
public List<WeaponLauncher> objectsCreated = new List<WeaponLauncher>();
public int objCounter { get; set; }

private void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (objCounter == 10)
            ChangeColor();
        else 
            Spawner();
    }
}

public void Spawner()
{
    objCounter++;

    int randObj = Random.Range(0, sourceObjects.Length);
    WeaponLauncher obj = sourceObjects[randObj]; // WeaponLauncher referencing a prefab

    float randX = Random.Range(-10, 10);
    float randY = Random.Range(-10, 10);
    Vector3 objPos = new Vector3(randX, randY, 0);

// ASK difference between prefab and WeaponLauncher referencing
    // Adding obj to List - throws an error when trying to change color
    // Instantiate(obj, objPos, Quaternion.identity);
    // objectsCreated.Add(obj);

    WeaponLauncher go = Instantiate(obj, objPos, Quaternion.identity);
    objectsCreated.Add(go);
}

public void ChangeColor()
{
    foreach (var go in objectsCreated)
        go.GetComponent<MeshRenderer>().material.color = Color.green;

    objectsCreated.Clear();
    Log("objCounter = " + objCounter);
    return;
}
***************** --------------- **********************

CODE EG Example of player connections
// When the player is connected, an id is assigned to him
public class Player
{
    public string playerName;
    public int id;

    public Player(int id, string name)
    {
        // Every time a player is connected, an id is generated
        this.id = id;
        playerName = name;
    }
}

public class MainClass : MonoBehaviour
{
    public Dictionary<int, Player> playerDictionary = new Dictionary<int, Player>();

    public Player p2;

    private void Start()
    {
        Player p1 = new Player(27, "Jon");
        p2 = new Player(49, "Kyle");
        Player p3 = new Player(73, "Susan");

        // Create a dictionary for easy access to each player's name
        // Use the player's id as the dictionary key
        playerDictionary.Add(p1.id, p1);
        playerDictionary.Add(p2.id, p2);
        playerDictionary.Add(p3.id, p3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var player =  playerDictionary[49];
            Log("Player name: " + player.playerName);
            Log("Player name: " + playerDictionary[27].playerName);

            var player2 = playerDictionary[p2.id];
            Log("Player 2 name: " + player2.playerName);

        }
    }
}
***************** --------------- **********************


CODE EG COROUTINES  
CODE EG Dissapear cube Routine *************
public bool isVisible;

private void Start()
{
    isVisible = true;
}

private void Update()
{
    if (Input.GetKeyDown(KeyCode.I) && isVisible)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        Log("Entering routine");
        StartCoroutine(HideShowRoutine());
        isVisible = false;
    }
}

IEnumerator HideShowRoutine()
{
    yield return new WaitForSeconds(5f);
    gameObject.GetComponent<Renderer>().enabled = true;
    Log("Exiting 5 seconds");
    isVisible = true;
}

***************** --------------- **********************

CODE EG Random Color Routine *****************

private void Start()
{
    StartCoroutine(RandomColorRoutine());
}

IEnumerator RandomColorRoutine(){
    while(true){
        gameObject.GetComponent<Renderer>().material.color = 
        new Color(Random.value, Random.value, Random.value);

        yield return new WaitForSeconds(3);
    }
}
------------------------------------------------------
private MeshRenderer mesh;
private WaitForSeconds waitTime = new WaitForSeconds(0.05f);

private void Start() {
    mesh = GetComponent<MeshRenderer>();
    StartCoroutine(ChangeColorRoutine());
}

private IEnumerator ChangeColorRoutine(){
    while(true){
        mesh.enabled = true;
        mesh.material.color = SetColor();
        yield return waitTime;
        mesh.enabled = false;
        yield return waitTime;
    }
}

private Color SetColor(){
    return new Color(Random.value, Random.value, Random.value);
}

***************** --------------- **********************

CODE EG Enemy detection *****************

https://discord.com/channels/675813882589085706/675824359897694259

https://discord.com/channels/675813882589085706/675824359897694259/843775417739640872

So you can have your enemies in a Layer called "EnemyLayer" (and Tag as "EnemyGuard" it's not necessary to use Tag but i will use it in the example)
And for the example lets assume that the enemy guard is  the"enemy" gameObject and has the "Enemy" component.

I am going to use Physics.OverlapSphere to find the enemy guards colliders near the guard that is alerting.
- enemy.transform.position is the enemy that wants to alert other guards and its going to serve as the center of the sphere.
- alertDistance is the distance that other guards can be alerted and its going to be the sphere radius.
- And at last i will use the layerMask i mentioned earlier because i want to search for enemy guards in that Layer only!(Using a layerMask for performance reasons)

float alertDistance = 10f;
Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, alertDistance, LayerMask.GetMask("EnemyLayer"));

foreach(Collider hitCollider in hitColliders)
{
    if(hitCollider.gameObject.tag == "EnemyGuard" && hitCollider.GetComponent<Enemy>() != enemy)
    { //Your code goes here S}
}

***************** --------------- **********************

CODE EG Set Model *****************

 private void SetModel()
{
    WeaponLauncher modelGo = Instantiate(model);
    modelGo.transform.SetParent(this.transform);
    modelGo.transform.localPosition = Vector3.zero;
    modelGo.transform.rotation = Quaternion.identity;
}

***************** --------------- **********************

CODE EG Make an enemy get the player position, change color with coroutine *****************

public class EndTrigger: Monobehaviour
{
    public delegate void onEndReachedAction(Vector3);
    public static event onEndReachedAction onEndReachedAction;
    
    private void OnTriggerEnter(Collider other)
    { if(other.tag == "Enemy") onEndReachedAction?.Invoke(); }
}

public class Mech: Monobehaviour
{
    void OnEnable() { EndTrigger.onEndReachedAction += FinishedWave; }    
    
    void OnDisable() { EndTrigger.onEndReachedAction -= FinishedWave; }
    
    public void FinishedWave(Vector3 newPos)
    {
        // if(transform.position = newPos) return;
        // transform.position = newPos;
        
        // float rand = Random.value;
        // Color newColor = new Color(rand,rand,rand);
        // _render.material.color = newColor;
        
        StartCoroutine(ColorChange());
    }  
    
    IEnumerator ColorChange(){
        yield return new WaitForSeconds(2);
        float rand = Random.value;
        Color newColor = new Color(rand,rand,rand);
        _render.material.color = newColor;
    }
}

***************** --------------- **********************

CODE EG Move Cube and Rotate with Mouse *****************

    private void MoveCube()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v);
        transform.Translate(input * speed * Time.deltaTime);
    }

    private void RotateCube()
    {
        float mouseX = Input.GetAxis("Mouse X") * 200 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 200 * Time.deltaTime;
        transform.Rotate(mouseX, mouseY, 0, Space.Self);
    }
***************** --------------- **********************

CODE EG Select objects with Mouse and Raycast *****************

public class RaycastIntoScene : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    
    // Useful for setting up values automatically
    private void OnValidate()
    { if (myCamera == null) myCamera = Camera.main; }

    private void Update()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Ray ray = myCamera.ScreenPointToRay(mouseScreenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.magenta);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
            raycastHit.collider.GetComponent<Renderer>().material.color = Color.cyan;
    }

    //**** another example
     void OnDrawGizmos(){
     Gizmos.matrix = this.transform.localToWorldMatrix;
     Gizmos.color = Color.red;
     Gizmos.DrawCube(Vector3.zero, Vector3.one);
     }
    
    ***************** --------------- **********************
    
    CODE EG Raycast single and multiple with cube *****************
    
    public class CubeLookingAtSphere : MonoBehaviour
    {
        [SerializeField] private Color singleColor = Color.green;
        [SerializeField] private Color allColor = Color.yellow;
        [SerializeField] private bool isMultiple;
        [SerializeField] private float speed;
    
        private void Update(){ RaycastSingle(); RaycastMultiple(); }
        private void RaycastSingle()
        {
            Vector3 origin = transform.position;
            var direction = transform.forward;
            Debug.DrawRay(origin, direction * 5f, Color.red);
            bool objectHit = Physics.Raycast(new Ray(origin, direction), out RaycastHit hitInfo);
            if (objectHit) hitInfo.collider.GetComponent<Renderer>().material.color = singleColor;
        }
        private void RaycastMultiple()
        {
            Vector3 origin = transform.position;
            var direction = transform.forward;
            Debug.DrawRay(origin, direction * 5f, Color.yellow);
            Ray ray = new Ray(origin, direction);
            var multipleHits = Physics.RaycastAll(ray);
            foreach (var raycastHit in multipleHits)
            { raycastHit.collider.GetComponent<Renderer>().material.color = allColor; }
        }
    }
    ***************** --------------- **********************
    
    CODE EG Raycast with LayerMask *****************
    
    public class CubeLookingAtSphereWithLayers : MonoBehaviour
    {
        private const float MAX_DISTANCE = 10f;
    
        [SerializeField] private Color raycastSingleTint = Color.green;
        [SerializeField] private Color raycastAllTint = Color.yellow;
        [SerializeField] private bool multiple;
        [SerializeField] private LayerMask layerMask;
    
        private void Update()
        {
            if (multiple) RaycastMultiple();
            else RaycastSingle();
        }
    
        private void RaycastSingle()
        {
            Vector3 origin = transform.position;
            var direction = transform.forward;
            Debug.DrawRay(origin, direction * 10f, Color.red);
            Ray ray = new Ray(origin, direction);
            bool objectHit = Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance:10f, layerMask);
            if (objectHit) raycastHit.collider.GetComponent<Renderer>().material.color = raycastSingleTint;
        }
        private void RaycastMultiple()
        {
            Vector3 origin = transform.position;
            var direction = transform.forward;
            Debug.DrawRay(origin, direction * 5f, Color.yellow);
            Ray ray = new Ray(origin, direction);
            var multipleHits = Physics.RaycastAll(ray, MAX_DISTANCE, layerMask);
            foreach (var raycastHit in multipleHits)
            { raycastHit.collider.GetComponent<Renderer>().material.color = raycastAllTint; }
        }
    }
    
    ***************** --------------- **********************
        
    CODE EG Configuring tags for a laser *****************
     
    [SerializeField] protected string parentTag;
    parentTag = transform.parent.tag;
 
    protected override void SetAdditionalValues()
    {
        if (transform.parent.CompareTag("PlayerStart"))
        {
            parentTag = "PlayerStart";
            laserMoveDirection = Vector3.up;
        }
        else if (transform.parent.CompareTag("Enemy"))
        {
            parentTag = "Enemy";
            laserMoveDirection = Vector3.down;
        }
        else
        {
            laserMoveDirection = Vector3.up; // Set direction to local
            Debug.Log("Parent tag not set in laser");
        }
    }

    ***************** --------------- **********************

    CODE EG How to stop a coroutine if needed *****************

        StartCoroutine(EnemyWaveRoutine());
        var wave2 = StartCoroutine(EnemyWaveRoutine());
        StopCoroutine(wave2); // stops just the 2nd EnemyWaveRoutine instance
        StopCoroutine("EnemyWaveRoutine"); // it will stop all EnemyWaveRoutine instances like GameObject.Find

    ***************** --------------- **********************

    CODE EG Set Direction and movement towards a target ********

    Move(){
    // this way the initial move will be very fast and later very slow...
    Vector3 moveDir = player.position - enemy.position;
    // this way the move will be constant
    Vector3 moveDir = (player.position - enemy.position).normalized;

    enemy.position += moveDir * speed * Time.deltaTime;
    }


    ***************** --------------- **********************

    CODE EG improved code to check bounds (mathf.clamp)*****

    float xPos = transform.position.x;
    float yPos = transform.position.y;

    if (yPos >= 0f) transform.position = new Vector2(xPos, 0f);
    else if (yPos <= -3.5f) transform.position = new Vector2(xPos, -3.5f);

    if (xPos >= 10.4f) transform.position = new Vector2(-10.4f, yPos);
    else if (xPos <= -10.4f) transform.position = new Vector2(10.4f, yPos);

    --------------

    var xPos = transform.position.x + Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
	var yPos = transform.position.y +Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

	xPos = xPos > 9.5f ? -9.5f : xPos;
	xPos = xPos < -9.5f ? 9.5f : xPos;

	var yMove = Mathf.Clamp(yPos, -4,4);

	transform.position = new Vector3(xPos, yMove, 0);

    ***************** --------------- **********************

    CODE EG




private void ScanForAttacks()
    {
        var hitInfo = Physics2D.CircleCast(transform.position, 2,transform.TransformDirection(Vector2.down), laserplayerLayermask);
        var hitCol = hitInfo.collider;
        if (hitCol == null || !hitCol.CompareTag("Laser")) return;
        StartCoroutine(AvoidPlayerLaserRoutine(hitInfo.point));
    }

    private IEnumerator AvoidPlayerLaserRoutine(Vector3 hitPoint)
    {
        var pos = transform.position;
        if (pos.y < hitPoint.y + 1)
            transform.Translate(pos.x += 1 * 3*Time.deltaTime,0,0);
        yield break;
    }




















*/
