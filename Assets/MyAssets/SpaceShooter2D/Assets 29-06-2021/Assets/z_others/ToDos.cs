using UnityEngine;

public class ToDos : MonoBehaviour { }

/*
 *
 * TEXT OR CS SCRIPT WITH INFO 
 * FOR EXAMPLE, THE NAME OF A SCENE
 * TO BE CREATED, CHANGED AND ACCESSED FROM THAT DOC
 * ADDRESS FOR OBJECTS, FOR EXAMPLE, PREFAB RESOURCES IN PHOTON 
 * 
 * PLAYER
 * Set laser firePoint parent
 * Set up multipleShotPrefab
 * Create laser prefabs pool
 * Play audio in laser
 *
 *
 * POWERUPS
 * Set powerups in switch
 * Fix PlayerStart and player declarations
 * Fix powerups Id's, set the powerup damage as the last
 * Set powerups as enum
 * 
 *
 * SPAWN MANAGER
 * Enemies stop spawning after some time
 *
 *
 * GENERAL
 * Implement reset procedure
 * 
 */
    
/*
 ENEMY
* Set firePoints with tag
* Implement events, Code to customize enemy in inspector
* create a Laser base class for laser, doubleLaser and Backwards Laser
* raycast for enemy and backward laser chasing player
* set values for each enemytype
* create weapons game objects
* make a weapons dictionary
* create a miscellaneous items dictionary (explosion, shield)
* create a doc in the project to explain how to create a new enemy with hardcode or in editor. if in editor, new empty, attach EnemyDefault script, set values, save as prefab

 Dictionary weapons Laser, DoubleLaser, BackwardLaser, Rocket
 if boss, create a weapon prefabs array
 Require component: rigidbody, 

isBoss
bossPositions

private void MoveEnemy2() --Zigzag
    {
    Vector2 vec = new Vector2(leftRightSpeed * enemyDirection, -1 * speed);
    transform.Translate(vec * Time.deltaTime);

    // set xpos = pos x
    // if posx > xpos + 2
    Vector2 xPos = transform.position;

    if (transform.position.x >= 2f) enemyDirection = -1;
    if (transform.position.x <= -2f) enemyDirection = 1;

    CheckBottomPosition();
}

speed = moveSpeed;
transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);

    In the start() function of EnemyMovement script, I put this:
8:59 AM
if (_enemy.GetEnemyType() == EnemyType.Infantry)
            InitInfantryMovementType();
        else if (_enemy.GetEnemyType() == EnemyType.Assault)
            InitAssaultMovementType();
        else if (_enemy.GetEnemyType() == EnemyType.Aggressor)
            InitAggressorMovementType();
9:00 AM
If the enemy is of type infantry or any other enemy type, then initialise a function that will choose a random movement type. (edited)
9:01 AM
so for instance, in the "InitInfatnryMovementType(), I put this:
9:01 AM
private void InitInfantryMovementType()
    {
        int randomIntValue = Random.Range(0, 2);
        if (randomIntValue == 0)
            _enemyMovementType = EnemyMovementType.Default;
        else if (randomIntValue == 1)
            _enemyMovementType = EnemyMovementType.ZigZag;
    }
9:02 AM
I created a random int value.If the random int value is 0, then I want the enemy of type Infantry to have the default movement type. if the random int is 1, I want the movement type to be zigzag.

GetEnemyType() returns the type of enemy. Here's what it is in code. Very simple.
public EnemyType GetEnemyType()
{
    return _enemyType;
}

private void Movement(){
    if(enemymoveType == EnemyMoveType.Default)
    transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);

    if(enemymoveType == EnemyMoveType.ZigZag)
    if(!canSwitch){ randomSide = Random.Range(1,3); canSwitch = true; }

    if(randomeSide == 1)
    transform.Translate((Vector3.down + _right).normalized * speed * Time.deltaTime);
    else if(randomeSide == 2)
    transform.Translate((Vector3.down + _left).normalized * speed * Time.deltaTime);

    if(enemymoveType == EnemyMoveType.Hover)
    if(transform.position.y >4f) { 
    transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);
    isHovering = true; }
    else if(isHovering) { 
    Vector3 newPos = new Vector3(transform.position.x, 4f,0);
    transform.position = newPos;
    StartCoroutine(HoverRoutine())
    }
    else transform.Translate(Vector3.down.normalized * hoverSpeed ...5... * Time.deltaTime
    }

    if (enemyMoveType == EnemyMove.Follow)
    {
        if (target == null) return;
        if (Vector3.Distance(transform.position,target.position) > 5f)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * time.DeltaTime);
    }

    if(enemyMove == EnemyMove.Aggressive) {
        isAggressive = true;
        transform.Translate(Vector3.down * (speed * 2) * Time.deltaTime);
    }
    else isAgressive = false;

    if(enemyMove == EnemyMove.Patrol){
        isPatrolling = true;
        Vector3 pos = transform.position;
        pos = Vector3.MoveTowards(transform.position, bossPositions[randomPoint].position, speed * Time.deltaTime);
        transform.position = pos;

        if(Vector3.Distance(pos, bossPositions[randomPoint].position)<0.2f)
        if(waitTime <=0)
            randomPoint = Random.Range(0, bossPositions.Length);
    }
}

*/
