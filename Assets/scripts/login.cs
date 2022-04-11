using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class login : MonoBehaviour
{
    public DependencyStatus dependencyStatus;    
    // public FirebaseUser User;
    public DatabaseReference DBreference;

    private string user_id;
    // Start is called before the first frame update
    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    void Start()
    {
        // this.user_id = gameObject.name;
        // this.user_id = "user1";
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform != null)
                {
                   
                    this.user_id = hit.transform.gameObject.transform.name;

                    print(this.user_id);
                    
                    int num = Random.Range(0,10);

                    // IDictionary<string, Object> childUpdates = new IDictionary<string, Object>();
                    // childUpdates["/user/" + this.user_id + "/cart"] = num;

                    // var DBTask = DBreference.UpdateChildrenAsync(childUpdates);

                    var DBTask = DBreference.Child("user").Child(this.user_id).Child("cart").SetValueAsync(num);

                    // yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                    if (DBTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                    }
                    else
                    {
                        print(num);
                    }
                }
            }
        }
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        // auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
}
