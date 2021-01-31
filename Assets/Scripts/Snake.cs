using System.Collections.Generic;
using UnityEngine;
using PlayNLearn;

public class Snake : MonoBehaviour
{
    public SpriteRenderer Picture;
    public Canvas canvas;
    private SnakeState state = SnakeState.Alive;
    private SnakeDirection gridMoveDirection = SnakeDirection.Right;// inicijalno da se pomeri udesno
    private Vector2Int gridPosition = new Vector2Int(10,10);
    private float gridMoveTimer = .25f;
    private float gridMoveTimerMax = .25f; // svake sekunde
    private LevelGrid levelGrid;
    private int snakeBodySize = 0;  // duzina zmije 0-samo glava
    private List<SnakeMovePosition> snakeMovePositionList = new List<SnakeMovePosition>();
    private List<SnakeBodyPart> snakeBodyPartList = new List<SnakeBodyPart>();

    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Update()
    {
        switch (state)
        {
            case SnakeState.Alive:           // ako je zmija ziva onda nastavlja svoje kretanje
                HandleInput();
                HandleGridMovement();
                break;
            case SnakeState.Dead:
                break;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != SnakeDirection.Down)
            {
                gridMoveDirection = SnakeDirection.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != SnakeDirection.Up)
            {
                gridMoveDirection = SnakeDirection.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != SnakeDirection.Right)
            {
                gridMoveDirection = SnakeDirection.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != SnakeDirection.Left)
            {
                gridMoveDirection = SnakeDirection.Right;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)                            // ukoliko imamo pozicije
            {
                previousSnakeMovePosition = snakeMovePositionList[0];   // uzima se prva kao prethodna
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition); // pre pomeranja se zapamti pozicija zmije u listu

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case SnakeDirection.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case SnakeDirection.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case SnakeDirection.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case SnakeDirection.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }

            gridPosition += gridMoveDirectionVector;

            gridPosition = levelGrid.ValidateGridPosition(gridPosition);        // da li je pozicija u granicama ekrana

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                // snake ate food, grow doby
                snakeBodySize++;
                CreateSnakeBody();
                //GameHandler.AddEquation();
                GetComponent<AudioSource>().Play();
                //AudioSource.PlayClipAtPoint
                /*			//GameObject's Collider is now a trigger Collider when the GameObject is clicked. It now acts as a trigger
							m_ObjectCollider.isTrigger = true;
							//Output to console the GameObject’s trigger state
							Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);*/
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)       // da li je lista prevelika u odnosu na velicinu zmije
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            /*for(int i=0; i<snakeMovePositionList.Count; i++)
			{
				Vector2Int snakeMovePosition = snakeMovePositionList[i];
				World_Sprite world_sprite = World_Sprite.Create(new Vector3(snakeMovePosition.x,snakeMovePosition.y), Vector3.one * .5f, Color.white);
				FunctionTimer.Create(world_sprite.DestroySelf,gridMoveTimerMax);	

			}*/

            // sa CreateSnakeBody se napravi niz sprajtova koje treba zalepiti na poziciju zmije (telo zmije)
            UpdateSnakeBodyParts();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)  // ako je pozicija glave ista kao pozicija tela
                {
                    // Zmija se sudarila - kraj
                    state = SnakeState.Dead;

                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);        // rotira se glava zmije


        }
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;

        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    // vraca poziciju cele zmije glava+telo
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        //gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Vector2Int gridPosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));

            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
            //kada se kreira dodaje se u listu
            //snakeBodyGameObject.Add(snakeBodyGameObject.transform);
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case SnakeDirection.Up:              // ako je trenutna pozicija gore
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0;
                            break;
                        case SnakeDirection.Left:    // ukoliko je prethodna pozicija bila ulevo
                            angle = 0 + 45;
                            break;
                        case SnakeDirection.Right:   // ukoliko je prethodna pozicija bila udesno
                            angle = 0 - 45;
                            break;
                    }
                    break;
                case SnakeDirection.Down:            // ako je trenutna pozicija dole
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180;
                            break;
                        case SnakeDirection.Left:    // ukoliko je prethodna pozicija bila ulevo
                            angle = 180 + 45;
                            break;
                        case SnakeDirection.Right:   // ukoliko je prethodna pozicija bila udesno
                            angle = 180 - 45;
                            break;
                    }
                    break;
                case SnakeDirection.Left:            // ako je trenutna pozicija ulevo
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90;
                            break;
                        case SnakeDirection.Down:    // ukoliko je prethodna pozicija bila dole	
                            angle = -45;
                            break;
                        case SnakeDirection.Up:      // ukoliko je prethodna pozicija bila gore
                            angle = 45;
                            break;
                    }
                    break;
                case SnakeDirection.Right:           // ako je trenutna pozicija udesno					
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 90;
                            break;
                        case SnakeDirection.Down:    // ukoliko je prethodna pozicija bila dole	
                            angle = 45;
                            break;
                        case SnakeDirection.Up:      // ukoliko je prethodna pozicija bila gore
                            angle = -45;
                            break;
                    }
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }

    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private SnakeDirection direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, SnakeDirection direction)
        {
            Debug.Log(gridPosition.x);
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }
        public SnakeDirection GetDirection()
        {
            return direction;
        }
        public SnakeDirection GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return SnakeDirection.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }

    }
}
