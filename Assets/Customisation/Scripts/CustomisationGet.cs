using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Debugging.Player
{
    public class CustomisationGet : MonoBehaviour
    {
        // Make all the variables.
        #region Variables.
        [Header("Player")]
        public Renderer characterRenderer;
        public GameObject player;
        public Movement m;
        [Header("Character Health Elements")]
        #region Player Health Variables.
        private float health;
        private float maxHealth;
        private float minHealth;
        private float healthRegen;
        public Image healthbar;
        public Text healthText;
        public Text healthRegenText;
        private int healthUseage;
        public Text healthUseageText;
        #endregion
        [Header("Character Mana Elements")]
        #region Player Mana Variables.
        private float mana;
        private float maxMana;
        private float minMana;
        private float manaRegen;
        public Image manabar;
        public Text manaText;
        public Text manaRegenText;
        #endregion
        [Header("Character Stamina Elements")]
        #region Player Stamina Variables.
        private float stamina;
        private float maxStamina;
        private float minStamina;
        private float staminaRegen;
        public Image staminabar;
        public Text staminaText;
        public Text staminaRegenText;
        #endregion
        [Header("Character Detail Text Boxes")]
        #region Player Details TextBoxes.
        public Text playerNameText;
        public TextMeshProUGUI strengthStatText;
        public TextMeshProUGUI dexterityStatText;
        public TextMeshProUGUI constitutionStatText;
        public TextMeshProUGUI wisdomStatText;
        public TextMeshProUGUI intelligenceStatText;
        public TextMeshProUGUI charismaStatText;
        public TextMeshProUGUI uniqueAbilityText;
        #endregion
        [Header("Character Stats and Details")]
        private int[] characterStats = new int[6];
        private string uniqueAbility;
        #endregion

        // Loads in player and their details at the Start.
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Load();
        }

        // Updates player stats and Regens them.
        void Update()
        {
            CheckStats();
            RegenStats();
            UpdateStatTexts();
        }

        // Loads up all the player details and prints them.
        void Load()
        {
            LoadStats();
            LoadTextures();
            UpdateStatTexts();
            EquateOtherStats();
        }

        // Loads Each Texture from file.
        private void LoadTextures()
        {
            SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
            SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
            SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
            SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
            SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
            SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        }

        // Displays the Textures on the player model.
        void SetTexture(string type, int index)
        {
            Texture2D texture = null;
            int matIndex = 0;
            switch (type)
            {
                case "Skin":
                    texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                    matIndex = 1;
                    break;
                case "Eyes":
                    texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                    matIndex = 2;
                    break;
                case "Mouth":
                    texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                    matIndex = 3;
                    break;
                case "Hair":
                    texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                    matIndex = 4;
                    break;
                case "Clothes":
                    texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                    matIndex = 5;
                    break;
                case "Armour":
                    texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                    matIndex = 6;
                    break;
            }

            Material[] mats = characterRenderer.materials;
            mats[matIndex].mainTexture = texture;
            characterRenderer.materials = mats;
        }

        // Updates all the players main texts.
        void UpdateStatTexts()
        {
            strengthStatText.text = (characterStats[0].ToString());
            dexterityStatText.text = (characterStats[1].ToString());
            constitutionStatText.text = (characterStats[2].ToString());
            wisdomStatText.text = (characterStats[3].ToString());
            intelligenceStatText.text = (characterStats[4].ToString());
            charismaStatText.text = (characterStats[5].ToString());
            uniqueAbilityText.text = uniqueAbility;
            playerNameText.text = player.name;
            healthUseage = 10;
        }

        // Loads in all the player stats from file.
        void LoadStats()
        {
            characterStats[0] = PlayerPrefs.GetInt("Strength");
            characterStats[1] = PlayerPrefs.GetInt("Dexterity");
            characterStats[2] = PlayerPrefs.GetInt("Constitution");
            characterStats[3] = PlayerPrefs.GetInt("Wisdom");
            characterStats[4] = PlayerPrefs.GetInt("Intelligence");
            characterStats[5] = PlayerPrefs.GetInt("Charisma");
            player.name = PlayerPrefs.GetString("characterName");
            uniqueAbility = PlayerPrefs.GetString("CharacterAbility");
            m.levelup(characterStats[1]);
            EquateOtherStats();
        }

        // Using the Stats from file it works out what the Health, Mana and Stamina Stats should be.
        private void EquateOtherStats()
        {
            #region Equate Health Stats.
            health = 10 + characterStats[2] * 4 + characterStats[0];
            maxHealth = health;
            minHealth = 0;
            healthRegen = characterStats[0];
            healthbar.fillAmount = 1;
            #endregion
            #region Equate Mana Stats.
            mana = 10 + characterStats[3] * 4 + characterStats[4];
            maxMana = mana;
            minMana = 0;
            manaRegen = characterStats[4];
            manabar.fillAmount = 1;
            #endregion
            #region Equate Stamina Stats.
            stamina = 10 + characterStats[5] * 4 + characterStats[1];
            maxStamina = stamina;
            minStamina = 0;
            staminaRegen = characterStats[1];
            staminabar.fillAmount = 1;
            #endregion
        }

        // Checks the Stats and updates the corrosponding UI Elements.
        private void CheckStats()
        {
            #region Check Health.
            float healthFraction = health / maxHealth;
            healthbar.fillAmount = healthFraction;
            healthText.text = (Mathf.RoundToInt(health) + "/" + Mathf.RoundToInt(maxHealth));
            healthRegenText.text = ("+" + Mathf.RoundToInt(healthRegen) + " Health per 5 seconds");
            #endregion
            #region Check Mana.
            float manaFraction = mana / maxMana;
            manabar.fillAmount = manaFraction;
            manaText.text = (Mathf.RoundToInt(mana) + "/" + Mathf.RoundToInt(maxMana));
            manaRegenText.text = ("+" + Mathf.RoundToInt(manaRegen) + " Mana per 5 seconds");
            #endregion
            #region Check Stamina.
            float staminaFraction = stamina / maxStamina;
            staminabar.fillAmount = staminaFraction;
            staminaText.text = (Mathf.RoundToInt(stamina) + "/" + Mathf.RoundToInt(maxStamina));
            staminaRegenText.text = ("+" + Mathf.RoundToInt(staminaRegen) + " Stamina per 5 seconds");
            #endregion
        }

        // Regenerates the Health, Mana and Stamina between frames.
        private void RegenStats()
        {
            #region Regen Health.
            if (health < maxHealth)
            {
                health += Time.deltaTime * (healthRegen / 5);
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
            #endregion
            #region Regen Mana.
            if (mana < maxMana)
            {
                mana += Time.deltaTime * (manaRegen / 5);
                if (mana > maxMana)
                {
                    mana = maxMana;
                }
            }
            #endregion
            #region Regen Stamina.
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime * (staminaRegen / 5);
                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
            }
            #endregion
        }

        // The button to use Health, Mana and Stamina.
        // Scene Reloads if you run out of health.
        public void UseStat(string stat)
        {
            switch (stat)
            {
                case "health":
                    health -= healthUseage;
                    if (health < minHealth)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    break;
                case "mana":
                    mana -= 10;
                    if (mana < minMana)
                    {
                        mana = minMana;
                    }
                    break;
                case "stamina":
                    stamina -= 10;
                    if (stamina < minStamina)
                    {
                        stamina = minStamina;
                    }
                    break;
                default:
                    print("incorect stat put in, put in: 'health', 'mana' or 'stamina'");
                    break;
            }
        }

        // Using the Base stats to upgrade Health, Mana and Stamina Stats.
        public void LevelUp()
        {
            #region Add Health Stats.
            maxHealth += characterStats[2];
            health = maxHealth;
            healthRegen += Mathf.RoundToInt(characterStats[0] / 3);
            #endregion
            #region Add Mana Stats.
            maxMana += characterStats[3];
            mana = maxMana;
            manaRegen += Mathf.RoundToInt(characterStats[4] / 3);
            #endregion
            #region Add Stamina Stats.
            maxStamina += characterStats[5];
            stamina = maxStamina;
            staminaRegen += Mathf.RoundToInt(characterStats[1] / 3);
            #endregion
            m.levelup(characterStats[1]);
        }
    }
}