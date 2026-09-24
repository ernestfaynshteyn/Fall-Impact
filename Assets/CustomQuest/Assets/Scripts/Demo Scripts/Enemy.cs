using UnityEngine;

/// <summary>
/// An example enemy class. Will take damage when colliding with a player, and when its health is 0, will process the criteria its a part of (If its part of a criteria)
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Field

    /// <summary>
    /// The health of this enemy
    /// </summary>
    public int health = 100;




    #endregion Field

    /// <summary>
    ///  Use this for initialization
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// Runs when this objects trigger colliders with another
    /// </summary>
    /// <param name="other">The other object colliding with this one</param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>())
        {

            if (health <= 0)
            { // Checks if this object is out of health, kills it if that is true
                health = 0;
                Destroy(this.gameObject);
            }
        }
    }
}