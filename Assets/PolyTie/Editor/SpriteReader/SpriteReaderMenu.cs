using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;

public class SpriteReaderMenu : Editor 
{
    [MenuItem("Assets/Import To Scene", true, 20001)]
    private static bool importToSceneEnabled()
    {
        for (var i = 0; i < Selection.objects.Length; ++i)
        {
            var obj = Selection.objects[i];
            var filePath = AssetDatabase.GetAssetPath(obj);
            if (filePath.EndsWith(".xml", System.StringComparison.CurrentCultureIgnoreCase))
                return true;
        }

        return false;
    }

    [MenuItem("Assets/Import To Scene", false, 20001)]
    private static void importToScene()
    {
        for (var i = 0; i < Selection.objects.Length; ++i)
        {
            var obj = Selection.objects[i];
            var filePath = AssetDatabase.GetAssetPath(obj);
            if (filePath.EndsWith(".xml", System.StringComparison.CurrentCultureIgnoreCase) == false)
                continue;
            loadLevelFromXML(filePath);
        }
    }

    /// <summary>
    /// Traverses the given xml file and created sprite
    /// objects within the current scene. The sprites 
    /// are positioned according to the x and y values
    /// specified in the Sprite node of the xml file.
    /// </summary>
    /// <param name="path">Path to the xml file</param>
    private static void loadLevelFromXML(string path)
    {
        var levelData = XDocument.Load(path);
        string levelName = levelData.Element("Level").Attribute("Name").Value;
        string directoryPath = System.IO.Path.GetDirectoryName(path);
        Debug.Log(string.Format("Loading level {0}", levelName));
        var sprites = new List<XElement>(levelData.Descendants("Sprite"));
        int spriteCount = sprites.Count;

        // Traverse xml document and load in sprites according to spatial data stored in xml file.
        var layers = levelData.Descendants("Layer");

        int layerIdx = 1;
        int spriteIdx = 0;
        foreach (var layer in layers)
        {
            string layerName = layer.Attribute("Name").Value;
            var goLayer = new GameObject(layerName);
            goLayer.transform.position = Vector3.zero;
            goLayer.AddComponent<FreezeHandle>();
            sprites = new List<XElement>(layer.Descendants("Sprite"));
            foreach (var sprite in sprites)
            {
                string spriteName = sprite.Attribute("Name").Value;

                // Display loading bar.
                spriteIdx++;
                EditorUtility.DisplayProgressBar("Importing level sprites", string.Format("Loading {0} sprite ...", spriteName), ((float)spriteIdx / (float)spriteCount));

                // Load spatial information.
                var position = new Vector3(float.Parse(sprite.Element("x").Value), float.Parse(sprite.Element("y").Value), 0f);

                string spritePath = string.Format("{0}/{1}", directoryPath, sprite.Element("FileName").Value);
                // Figure out if we need to change max texture size;
                var spriteInfo = TextureImporter.GetAtPath(spritePath) as TextureImporter;
                int spriteWidth = int.Parse(sprite.Attribute("Width").Value);
                int spriteHeight = int.Parse(sprite.Attribute("Height").Value);

                var spriteSettings = new TextureImporterSettings();
                spriteInfo.ReadTextureSettings(spriteSettings);
                bool dirty = false;
                if (spriteInfo.textureType != TextureImporterType.Sprite)
                {
                    spriteSettings.ApplyTextureType(TextureImporterType.Sprite, true); 
                    dirty = true;
                }
                if ((spriteWidth > 1024 || spriteHeight > 1024) && spriteSettings.maxTextureSize < 2048)
                {
                    spriteSettings.maxTextureSize = 2048;
                    dirty = true;
                }
                if ((spriteWidth > 2048 || spriteHeight > 2048) && spriteSettings.maxTextureSize < 4096)
                {
                    spriteSettings.maxTextureSize = 4096;
                    dirty = true;
                }
                if (dirty == true)
                {
                    spriteInfo.SetTextureSettings(spriteSettings);
                    AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);
                }

                // Load texture data specified in xml file.
                var objSprite = Resources.LoadAssetAtPath<Sprite>(spritePath);
                objSprite.name = spriteName;

                // Setup sprite game object
                var goSprite = new GameObject(spriteName);
                var renderer = goSprite.AddComponent<SpriteRenderer>();
                renderer.sprite = objSprite;
                renderer.sortingLayerID = layerIdx;

                // Link to current layer.
                goSprite.transform.parent = goLayer.transform;

                // Get sprite pixelToUnit information to position game object accordingly.
                goSprite.transform.position = (position / spriteInfo.spritePixelsToUnits);
            }
            layerIdx++;
        }
        EditorUtility.ClearProgressBar();
    }
}
