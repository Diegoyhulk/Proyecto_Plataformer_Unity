using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public Vector3 SavedPosition { get; private set; }
        public Vector3 SavedOrientation {get; private set;}
        public static GameManager instance{get; private set;}

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadNewScene(int targetIndex, Vector3 targetOrientation, Vector3 targetPosition)
        {
            SavedPosition = targetPosition;
            SavedOrientation = targetOrientation;
            SceneManager.LoadScene(targetIndex);
        }
    }
}