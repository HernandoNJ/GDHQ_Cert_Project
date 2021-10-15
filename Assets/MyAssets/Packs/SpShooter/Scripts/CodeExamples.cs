public class CodeExamples { }

// ******* Increase, decrease speed **********

//// if Key S, increase speed
//if (Input.GetKeyDown(KeyCode.S)) speed += 5f;

//// if Key A, decrease speed
//if (Input.GetKeyDown(KeyCode.A)) speed -= 5f;

//// if speed > 20, print out "Slow down"
//// speed can't be lower than 0
//if (speed > 20f) Debug.Log("Slow down");
//else if (speed <= 0f)
//{
//    speed = 0f;
//}

//// if speed = 0, print out "Speed up"
//if (speed == 0f) Debug.Log("Speed Up");

// ---------------------------------------



// ************** Change color if score > 50 ****************

//[SerializeField] private int score;
//[SerializeField] private GameObject playerGO;
//[SerializeField] private bool isGreenColorSet;

// playerGO.GetComponent<MeshRenderer>().material.color = Color.red;

//if (Input.GetKeyDown(KeyCode.Space)) score += 10;

//if (score > 50 && isGreenColorSet == false)
//{
//    playerGO.GetComponent<MeshRenderer>().material.color = Color.green;
//    Debug.Log("Color changed to green");
//    isGreenColorSet = true;
//} ----------------------------------------



// ************** Calculate grades average **********************

//[SerializeField] private int points, grade1, grade2, grade3, grade4, grade5, gradesTotal;
//[SerializeField] private float averageGrade;
//[SerializeField] private bool sentMessage;

//void Start()
//{
//    grade1 = RandomGrade();
//    grade2 = RandomGrade();
//    grade3 = RandomGrade();
//    grade4 = RandomGrade();
//    grade5 = RandomGrade();

//    gradesTotal = grade1 + grade2 + grade3 + grade4 + grade5;

//    averageGrade = Mathf.Round(gradesTotal / 4f * 100f) * 0.01f;
//}

//void Update()
//{
//    if (averageGrade > 90 && sentMessage == false)
//    { Debug.Log("A"); sentMessage = true; }

//    else if (averageGrade >= 80 && averageGrade < 90 && sentMessage == false)
//    { Debug.Log("B"); sentMessage = true; }

//    else if (averageGrade >= 70 && averageGrade < 80 && sentMessage == false)
//    { Debug.Log("C"); sentMessage = true; }

//    else if (sentMessage == false)
//    { Debug.Log("F"); sentMessage = true; }
//}

//public int RandomGrade()
//{
//    int n = Random.Range(1, 100);
//    return n;
//}


// *************** Move Player in restricted area ***************

//public void MovePlayer()
//{
//    // translate the player with horiz and vert
//    float horizontal = Input.GetAxis("Horizontal");
//    float vertical = Input.GetAxis("Vertical");

//    Vector2 moveDirection = new Vector2(horizontal, vertical, 0).normalized;

//    transform.Translate(moveDirection * speed * Time.deltaTime);

// *** Restriction 1 ***
//    float xPos = transform.position.x;
//    float yPos = transform.position.y;

//    if (yPos >= 0f) transform.position = new Vector2(xPos, 0f, 0f);
//    else if (yPos <= -3.5f) transform.position = new Vector2(xPos, -3.5f, 0f); 

//    if (xPos >= 10.4f) transform.position = new Vector2(-10.4f, yPos, 0f);
//    else if (xPos <= -10.4f) transform.position = new Vector2(10.4f, yPos, 0f); 

// *** Restriction 2 ***
//float xClamp = Mathf.Clamp(transform.position.x, -10f, 10f);
//float yClamp = Mathf.Clamp(transform.position.y, -3f, 0f);
//transform.position = new Vector2(xClamp, yClamp, 0);
//} ------------------------------------



