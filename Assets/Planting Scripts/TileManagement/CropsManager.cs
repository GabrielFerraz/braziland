using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CropTile
{
    public int growTimer;
    public int growStage;

    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

    private GameObject goCrop;
    public bool Complete
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }
    public void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        if (renderer != null)
        {
            renderer.gameObject.SetActive(false);
        }
        damage = 0;
    }
}

    public class CropsManager : TimeAgent
    {
        [SerializeField] TileBase plowed;
        [SerializeField] TileBase seeded;
        [SerializeField] Tilemap targetTilemap;
        [SerializeField] GameObject cropsSpritePrefab;

        [SerializeField] GameObject collectable;
        Dictionary<Vector2Int, CropTile> crops;
        private CropTile crop;
    private GameObject goCrop;


    private void Start()
        {
            crops = new Dictionary<Vector2Int, CropTile>();
            onTimeTick += Tick;
            Init();
        }

        private void Tick()
        {
            foreach (CropTile cropTile in crops.Values)
            {
                if (cropTile.crop == null)
                    continue;

                cropTile.damage += 0.02f;

                if (cropTile.damage > 1f)
                {
                    cropTile.Harvested();
                    targetTilemap.SetTile(cropTile.position, plowed);
                    continue;
                }
                if (cropTile.Complete)
                {
                    //Debug.Log("I'm done growing");
                    cropTile.crop = null;
                    GameObject go = Instantiate(collectable);
                    go.transform.position = targetTilemap.CellToWorld(cropTile.position);
                    go.transform.position = new Vector3(cropTile.position.x + 0.52f, cropTile.position.y + 0.52f, cropTile.position.z - 0.2f);
                    crop.renderer = null;
                if (cropsSpritePrefab != null)
                {
                    if (goCrop != null)
                    {
                        Destroy(goCrop);
                    }
                }
               
                continue;
                }
                cropTile.growTimer += 1;

                if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    cropTile.renderer.gameObject.SetActive(true);
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                    cropTile.growStage += 1;
                }


            }
        }

        public bool Check(Vector3Int position)
        {

            return crops.ContainsKey((Vector2Int)position);
        }
        public void Plow(Vector3Int position)
        {
            if (crops.ContainsKey((Vector2Int)position))
            {
                return;
            }

            CreatePlowedTile(position);

        }
        public void Seed(Vector3Int position, Crop toSeed)
        {
            targetTilemap.SetTile(position, seeded);
            crops[(Vector2Int)position].crop = toSeed;
        }

        public void CreatePlowedTile(Vector3Int position)
        {
            crop = new CropTile();
            crops.Add((Vector2Int)position, crop);


            if (cropsSpritePrefab!= null)
            {
                goCrop = Instantiate(cropsSpritePrefab);
            goCrop.transform.position = targetTilemap.CellToWorld(position);
            goCrop.transform.position = new Vector3(position.x + 0.52f, position.y + 0.52f, position.z - 0.2f);

            goCrop.SetActive(false);

                crop.renderer = goCrop.GetComponent<SpriteRenderer>();

                crop.position = position;

                targetTilemap.SetTile(position, plowed);
            }
          
        }

        public void Pickup(Vector3Int gridPosition)
        {
            Vector2Int position = (Vector2Int)gridPosition;
            if (crops.ContainsKey(position) == false) { return; }

            CropTile cropTile = crops[position];

            if (cropTile.Complete)
            {
                ItemSpawnManager.instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                GameManager.instance.itemManager.GetItemByName(cropTile.crop.yield.itemName),
                cropTile.crop.count
                );
            }
            targetTilemap.SetTile(gridPosition, plowed);
            cropTile.Harvested();
        }
    }



