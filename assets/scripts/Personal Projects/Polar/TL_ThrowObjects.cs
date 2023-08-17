using UnityEngine;

public class TL_ThrowObjects : MonoBehaviour
{
    private Animator CharacterAnimator;
    private TL_GrabObjects GrabObjectsScript;
    private TL_SwapAbilities SwapAbilitiesScript;


    void Start()
    {
        CharacterAnimator = GetComponent<Animator>();
        GrabObjectsScript = GetComponent<TL_GrabObjects>();
        SwapAbilitiesScript = GetComponent<TL_SwapAbilities>();
    }

    //Throws the picked up object
    void ThrowObject()
    {
        //If the throw button is pressed while the character is holding an object
        if (Input.GetKeyDown(KeyCode.T) && GrabObjectsScript.ReturnPickedUpObject() != null)
        {
            //Set the trigger to true
            CharacterAnimator.SetBool("IsThrowing", true);
            
            //Set the trigger to false
            CharacterAnimator.SetBool("IsGrabbing", false);

            //Obtain the rigidbody from the picked up object
            Rigidbody ObjectRigidbody = GrabObjectsScript.ReturnPickedUpObject().GetComponent<Rigidbody>();

            var boxColliders = ObjectRigidbody.GetComponents<BoxCollider>();

            foreach (var box in boxColliders)
            {
                box.enabled = true;
            }

            //Add the force to throw the object
            ObjectRigidbody.AddForce(transform.forward * 1500f);

            //Reset the values of the picked up object
            GrabObjectsScript.ResetObjectProperties(GrabObjectsScript.ReturnPickedUpObject());

            //Switch the toggle off
            GrabObjectsScript.GrabToggle = false;

            //Update the dropped object
            GrabObjectsScript.ReturnPickedUpObject();

            //Set the trigger to false
            CharacterAnimator.SetBool("IsThrowing", false);

            //Re-enable the swap abilities script
            SwapAbilitiesScript.enabled = true;
        }
    }

    void Update()
    {
        ThrowObject();
    }

}
