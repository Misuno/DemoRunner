using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunnerLevelLogic : MonoBehaviour
{
    // Singleton.
    public static RunnerLevelLogic Instance;
	
    // Initial number of level parts.
    public int visibleParts = 10;

    // Balance stuff.
    // Lower difficulty value is more difficult to play.
    public float initialDifficulty = 0.8f;
    public float maxDifficulty = 0.3f;

    // How often we change difficulty and speed.
    public float difficultyUpdatePeriod = 3f;

    // Change values for difficulty and speed.
    public float changeSpeedRatio = 1f;
    public float changeDifficultyRatio = 1f;

    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;

    // What parts could be generated on this particular level.
    public List<LevelPart> levelPartPrefabs;

    // Current difficulty level.
    private float difficultyLevel;

    public float DifficultyLevel {
        get { return difficultyLevel; }
        set
        {
            difficultyLevel = value;
            character.MoveSpeedMultiplier += changeSpeedRatio;
        }
    }

    // For purposes of smarter level generation we have to group obstacles by their types.
    private Dictionary< PartType, List<LevelPart> > partsByTypes = new Dictionary<PartType, List<LevelPart>>();

    // Helper variables for generation.
    private LevelPart lastGeneratedPart;
    private Queue<LevelPart> generatedParts = new Queue<LevelPart>();

    private RunnerLevelUI ui;

    void Awake ()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        GeneratePartList();

        // Must initialise difficulty.
        DifficultyLevel = initialDifficulty;

        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<RunnerLevelUI>();
		
    }

    // Use this for initialization
    void Start ()
    {
        Stats.Instance.Reset();
        GenerateLevelParts(visibleParts);

        // This coroutine will update difficulty level and speed periodically.
        StartCoroutine(UpdateDifficulty());
    }

    /// <summary>
    /// Generates the part list.
    /// </summary>
    void GeneratePartList ()
    {
        foreach (var part in levelPartPrefabs)
        {
            var type = part.type;

            if (!partsByTypes.ContainsKey(type))
            {
                partsByTypes.Add(type, new List<LevelPart>());
            }

            partsByTypes[part.type].Add(part);
        }
    }

    /// <summary>
    /// Generates specified number of level parts.
    /// </summary>
    /// <param name="partsNumber">Parts number.</param>
    public void GenerateLevelParts (int partsNumber)
    {
        for (var i = 0; i < partsNumber; ++i)
        {
            int rnd;
            LevelPart newPart;

            // If we have something generated, place new part after it.
            if (lastGeneratedPart != null)
            {
                // We can generate only parts to which we can connect.
                // So we neet to chose one type first.
                rnd = Random.Range(0, lastGeneratedPart.canConnectTo.Count);
                var selectedType = lastGeneratedPart.canConnectTo[rnd];

                // Then we chose one part of selected type and create it.
                var listPartOfType = partsByTypes[selectedType];
                var count = listPartOfType.Count;
                rnd = Random.Range(0, count);
                var neededPart = listPartOfType[rnd];

                newPart = Instantiate<LevelPart>(neededPart);

                // And place it after the previous part.
                newPart.transform.position = lastGeneratedPart.transform.position + new Vector3(0f, 0f, newPart.length);
            } 
			// If it is first part we can generate whatever we want.
			// Then we place it to zero coordinates.
			else
            {
                rnd = Random.Range(0, levelPartPrefabs.Count);
                newPart = Instantiate<LevelPart>(levelPartPrefabs[rnd]);
                newPart.transform.position = Vector3.zero;
            }

            // And store generated part.
            generatedParts.Enqueue(newPart);
            lastGeneratedPart = newPart;
        }

        // After generating new parts we have to cut the level.
        CutLevel(1.5f);
    }

    /// <summary>
    /// Deletes last parts
    /// </summary>
    /// <param name="cutCoeff">Multiplier of initial number of parts.</param>
    void CutLevel (float cutCoeff)
    {
        // We have to leave more than was originally generated.
        var partsToLeave = visibleParts;
        if (cutCoeff > 1f)
        {
            partsToLeave = (int)(partsToLeave * cutCoeff);
        }

        // Cut extra parts.
        while (generatedParts.Count > partsToLeave)
        {
            Destroy(generatedParts.Dequeue().gameObject);
        }
    }

    /// <summary>
    /// Periodically updates the difficulty.
    /// </summary>
    IEnumerator UpdateDifficulty ()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyUpdatePeriod);
            var tmp = DifficultyLevel - changeDifficultyRatio;
            DifficultyLevel = Mathf.Max(tmp, maxDifficulty);
        }
    }

    /// <summary>
    /// Event when player gets a coin.
    /// </summary>
    /// <param name="value">coin value.</param>
    public void PlayerGotCoinWithValue (int value)
    {
        Stats.Instance.coins += value;
        ui.SetCoinsText(Stats.Instance.coins);
    }


    void Test ()
    {
        var dict = new Dictionary<string, int>();

        dict.Add("one", 1);
        dict.Add("two", 2);
        dict.Add("three", 5);

        var uno = dict["one"]; // uno = 1
        dict["three"] = 3;

        Dictionary<States, Dictionary<Types, List<LevelPart>>> superDict = new Dictionary<States, Dictionary<Types, List<LevelPart>>>();

        var stateTypesDict = superDict[States.Nevada];
        var partList = stateTypesDict[Types.Desert];
        var part = partList[0];

    }

    enum States
    {
        Nevada,
        Texas
    }

    enum Types
    {
        Mountains,
        Desert,
        Town
    }
}
