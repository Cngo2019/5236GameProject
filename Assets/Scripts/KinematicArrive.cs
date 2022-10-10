using UnityEngine;

public class KinematicArrive
{
    const float RADIUS = .01f;
    const float TRAVEL_TIME = 1f;
    

    /**
    This method gives us the velocity based on the kinematic arrive algorithm.
    Note: The velocity is the only thing being returned because I found a way to orient the character without needing a
    steering output object.

    This method is called every frame when the character is in the moving state.
    **/
    public static Vector2 getSteering(Vector2 characterPosition, Vector2 targetPosition, float minSpeed, float maxSpeed) {
        // Calculate the steeringVelocity vector
        Vector3 steeringVelocity = targetPosition - characterPosition;
        
        // If the object is within range then return a 0 steeringVelocity to stop the rigid body character
        if (steeringVelocity.magnitude <= RADIUS) {
            return Vector2.zero;
        }
        
        // Divide the steeringVelocity over time (cut it up)
        steeringVelocity /= TRAVEL_TIME;
        
        // If the steeringVelocity exceeds the speed then constrain it 
        if (steeringVelocity.magnitude > maxSpeed) {
            steeringVelocity.Normalize();
            steeringVelocity *= maxSpeed;
        }

        if (steeringVelocity.magnitude < minSpeed) {
            steeringVelocity.Normalize();
            steeringVelocity *= minSpeed;
        }
    
        // Return the updated velocity.
        return steeringVelocity;
    }

}
