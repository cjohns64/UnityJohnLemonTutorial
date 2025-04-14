## Completed Unity John Lemon Tutorial with additional modifications

[Completed John Lemon Tutorial](https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-learn-3d-beginner-john-lemon-complete-project-143846)

In this project I added several elements to the completed tutorial. The goal was to add 4 new elements that included:

- At least one gameplay element that uses a dot product.
- At least one gameplay element that uses linear interpolation.
- At least one new particle effect with trigger.
- At least one new sound effect with trigger.

What I added:
- Replaced the capsule collider detection system on the enemies with a vision cone calculated with a dot product.
- Added a "caught" meter that fills when an enemy can see the player and drains when the player is hidden.
- The caught meter fills faster the closer the player is to an enemy, this is the linear interpolation element.
- Added a particle system to the ghosts that will add to the caught meter if the player collides with a particle.
- Added a sound effect (from freesound.org) that plays when the player collides with one of the above particles.
