using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{

    [SerializeField] private Horse horse;
    [SerializeField] private FixedJoint2D joint;
    [SerializeField] private BoxCollider2D collider2D; 
    public SpriteRenderer enchantment;
    public int value;
    public bool hit;
    
    public bool enchanted;
    
    public bool Hit(float magnitude)
    {
        if (hit)
        {
            return false;
        }
        

        if (magnitude > SharpBit.requiredMagnitude)
        {
            joint.enabled = false;
            hit = true;
            horse.armourRemaining--;
            collider2D.size = collider2D.size / 2;
            enchantment.gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public void Enchant()
    {
        Invoke("InvokedEnchant",0);
    }


    private void InvokedEnchant()
    {
        enchantment.gameObject.SetActive(true);

        Sprite enchantmentSprite = horse.player.selectedCharge.armourEnchantments[value];
        enchanted = true;
        enchantment.sprite = enchantmentSprite;
    }
    public void ReattachToHorse()
    {
        joint.enabled = true;
    }
    
    
}
