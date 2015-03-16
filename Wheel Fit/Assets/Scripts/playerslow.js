	// collision event script attached to a ParticleSystem
	// applies a force to rigid bodies that are hit by particles
	private var collisionEvents = new ParticleSystem.CollisionEvent[16];
	function OnParticleCollision(other : GameObject)
 {
		// adjust array length
		var safeLength = particleSystem.safeCollisionEventSize;
		if (collisionEvents.Length < safeLength) 
		{
			collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
		}
		// get collision events for the gameObject that the script is attached to
		var numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
		// apply some force to RigidBody components
		for (var i = 0; i < numCollisionEvents; i++) {
			if (other.rigidbody)
			{
	 		var pos = collisionEvents[i].intersection;
			var force = collisionEvents[i].velocity * 5;
			other.rigidbody.AddForce(Vector3.back * 12.5);
			}
		}
	}
	