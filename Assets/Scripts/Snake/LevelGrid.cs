using UnityEngine;

namespace PlayNLearn
{
    public class LevelGrid
    {
        private Vector2Int foodGridPosition;
        private GameObject foodGameObject;
        private int width;
        private int height;
        private Snake snake;

        public LevelGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Setup(Snake snake)
        {
            this.snake = snake;
            SpawnFood();
        }

        private void SpawnFood()
        {
            do
            {
                // random pozicija jabuke
                foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            }
            while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);           // uslov da se nova jabuka ne iscrta preko zmije

            foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);

            //collider = new BoxCollider2D();
            //BoxCollider2D boxColider = new BoxCollider2D();
            foodGameObject.AddComponent(typeof(BoxCollider2D));
            foodGameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            //foodGameObject.AddComponent(typeof(HitCollider));

            /*
		    //We have a string holding a script name
		    string ScriptName = "HitCollider";
		    //We need to fetch the Type
		    System.Type MyScriptType = System.Type.GetType(ScriptName + ",Assembly-UnityScript");
		    //Now that we have the Type we can use it to Add Component

		    foodGameObject.AddComponent(MyScriptType);
		    //collider = GetComponent<Collider>();*/

        }

        public bool TrySnakeEatFood(Vector2Int snakeGridPostion)
        {
            if (snakeGridPostion == foodGridPosition)
            {
                Object.Destroy(foodGameObject); // pojede jabuku
                SpawnFood();                    // kreira novu jabuku ponovo
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
        {
            if (gridPosition.x < 0)
            {
                gridPosition.x = width - 1; // posto je sirina 20 onda ide od 0-19 znaci ako je skroz levo vraca je skroz desno
            }
            if (gridPosition.x > width - 1) // ukoliko izlazi sa desne strane zmijica		
            {
                gridPosition.x = 0;         // vrati zmijicu na levu stranu
            }
            if (gridPosition.y < 0)
            {
                gridPosition.y = height - 1;
            }
            if (gridPosition.y > height - 1)
            {
                gridPosition.y = 0;
            }
            return gridPosition;
        }
    }
}