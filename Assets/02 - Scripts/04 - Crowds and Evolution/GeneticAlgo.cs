using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneticAlgo : MonoBehaviour
{

    [Header("Genetic Algorithm parameters")]
    public int popSize = 100;
    public GameObject animalPrefab;

    [Header("Dynamic elements")]
    public float vegetationGrowthRate = 1.0f;
    public float currentGrowth;
    private List<GameObject> animals;
    [UPyPlot.UPyPlotController.UPyProbe]
    private float animalNum;
    protected Terrain terrain;
    protected CustomTerrain customTerrain;

    [UPyPlot.UPyPlotController.UPyProbe]
    private float grassNum;
    [SerializeField] float grassTotal = 1000f;
    protected float width;
    protected float height;

    [SerializeField] float slopeGrassThreshold = 30f;

    void Start()
    {
        // Retrieve terrain.
        terrain = Terrain.activeTerrain;
        customTerrain = GetComponent<CustomTerrain>();
        width = terrain.terrainData.size.x;
        height = terrain.terrainData.size.z;

        // Initialize terrain growth.
        currentGrowth = 0.0f;

        // Initialize animals array.
        animals = new List<GameObject>();
        for (int i = 0; i < popSize; i++)
        {
            GameObject animal = makeAnimal();
            animals.Add(animal);
        }
    }

    void Update()
    {
        // Keeps animal to a minimum.
        while (animals.Count < popSize / 2)
        {
            animals.Add(makeAnimal());
        }
        grassNum = (float)customTerrain.getDetailCoverage();
        animalNum = (float)animals.Count;
        customTerrain.debug.text = "No animals: " + animalNum + "\nGrass coverage: " + grassNum + "/" + grassTotal;

        // Update grass elements/food resources.
        updateResources();
    }

    /// <summary>
    /// Method to place grass or other resource in the terrain.
    /// </summary>
    public void updateResources()
    {
        Vector2 detail_sz = customTerrain.detailSize();
        Vector3 grid_sz = customTerrain.gridSize();
        int[,] details = customTerrain.getDetails();
        currentGrowth += vegetationGrowthRate;
        while (currentGrowth > 1.0f && customTerrain.getDetailCoverage() < grassTotal)
        {
            int x = (int)(UnityEngine.Random.value * detail_sz.x);
            int y = (int)(UnityEngine.Random.value * detail_sz.y);

            float slope = customTerrain.getSteepness((float)x * grid_sz.x / detail_sz.x, (float)y * grid_sz.z / detail_sz.y);
            float height = customTerrain.getInterp((float)x * grid_sz.x / detail_sz.x, (float)y * grid_sz.z / detail_sz.y);
            if (Mathf.Abs(slope) >= slopeGrassThreshold || height < customTerrain.waterLevel)
            {
                continue;
            }

            details[y, x] = 1;
            customTerrain.detailCoverageIncre(1);
            currentGrowth -= 1.0f;

        }
        customTerrain.saveDetails();
    }

    /// <summary>
    /// Method to instantiate an animal prefab. It must contain the animal.cs class attached.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject makeAnimal(Vector3 position)
    {
        GameObject animal = Instantiate(animalPrefab, transform);
        animal.name = "Animal" + System.Guid.NewGuid().ToString();
        animal.GetComponent<Animal>().Setup(customTerrain, this);
        animal.transform.position = position;
        animal.transform.Rotate(0.0f, UnityEngine.Random.value * 360.0f, 0.0f);
        return animal;
    }

    /// <summary>
    /// If makeAnimal() is called without position, we randomize it on the terrain.
    /// </summary>
    /// <returns></returns>
    public GameObject makeAnimal()
    {
        Vector3 scale = terrain.terrainData.heightmapScale;
        float x = UnityEngine.Random.value * width;
        float z = UnityEngine.Random.value * height;

        // if slope is too high, try again
        while (customTerrain.getSteepness(x, z) > slopeGrassThreshold)
        {
            x = UnityEngine.Random.value * width;
            z = UnityEngine.Random.value * height;
        }

        float y = customTerrain.getInterp(x, z);

        if(y < customTerrain.waterLevel) {
            return makeAnimal();
        }

        return makeAnimal(new Vector3(x, y + 0.5f, z));
    }

    /// <summary>
    /// Method to add an animal inherited from anothed. It spawns where the parent was.
    /// </summary>
    /// <param name="parent"></param>
    public void addOffspring(Animal parent)
    {
        GameObject animal = makeAnimal(parent.transform.position);
        animal.GetComponent<Animal>().InheritBrain(parent.GetBrain(), true);
        animals.Add(animal);
    }

    /// <summary>
    /// Remove instance of an animal.
    /// </summary>
    /// <param name="animal"></param>
    public void removeAnimal(Animal animal)
    {
        animals.Remove(animal.transform.gameObject);

        // if the animal has QuadrupedProceduralMotion, remove it and its goal
        QuadrupedProceduralMotion motion = animal.GetComponentInChildren<QuadrupedProceduralMotion>();
        if (motion != null)
        {
            Destroy(motion.goal.gameObject);
            Destroy(motion);
        }

        Destroy(animal.transform.gameObject);

        // get component FabricIK in children
        //FabricIK[] ikComponents = animal.GetComponentsInChildren<FabricIK>();
        //DestroyImmediate(animal.transform.gameObject);
    }

}
