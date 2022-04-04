using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class PoisonSurface : MonoBehaviour
    {
        public float poisonBuildUpAmount = 7;

        public List<CharacterEffectManager> charactersInsideSurface;

        private void OnTriggerEnter(Collider other)
        {
            CharacterEffectManager character = other.GetComponentInChildren<CharacterEffectManager>();

            if(character != null)
            {
                charactersInsideSurface.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {

            CharacterEffectManager character = other.GetComponent<CharacterEffectManager>();

            if (character != null)
            {
                charactersInsideSurface.Remove(character);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach(CharacterEffectManager character in charactersInsideSurface)
            {
                if(!character.isPoisoned)
                {
                    character.poisonBuildup = character.poisonBuildup + poisonBuildUpAmount * Time.deltaTime; 
                }
            }
        }
    }
}
