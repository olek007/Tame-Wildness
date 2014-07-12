using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

[RequireComponent(typeof(Camera))]
public class LayoutExporterButton : MonoBehaviour 
{
    private bool _isShowingGUI = true;

    void OnGUI()
    {
        if (_isShowingGUI == true)
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "Export Layout") == true)
                StartCoroutine(exportLayout());
        }
    }

    private IEnumerator exportLayout()
    {
        _isShowingGUI = false;
        yield return new WaitForEndOfFrame();
        // Figour out size of all collider bodies.
        float xMin = float.PositiveInfinity; float xMax = float.NegativeInfinity;
        float yMin = float.PositiveInfinity; float yMax = float.NegativeInfinity;

        var allPolies = FindObjectsOfType<PolygonCollider2D>();
        foreach (var poly in allPolies)
        {
            var vertices = poly.GetWorldPoints();
            for (int i = 0; i < vertices.Length; i++)
            {
                var v = vertices[i];
                if (v.x < xMin)
                    xMin = v.x;
                if (v.x > xMax)
                    xMax = v.x;
                if (v.y < yMin)
                    yMin = v.y;
                if (v.y > yMax)
                    yMax = v.y;
            }
        }
        var allCircles = FindObjectsOfType<CircleCollider2D>();
        foreach (var circle in allCircles)
        {
            var left = circle.transform.position.x - circle.radius;
            var right = circle.transform.position.x + circle.radius;
            var top = circle.transform.position.y + circle.radius;
            var bottom = circle.transform.position.y - circle.radius;
            if (left < xMin)
                xMin = left;
            if (left > xMax)
                xMax = left;
            if (right < xMin)
                xMin = right;
            if (right > xMax)
                xMax = right;
            if (top < yMin)
                yMin = top;
            if (top > yMax)
                yMax = top;
            if (bottom < yMin)
                yMin = bottom;
            if (bottom > yMax)
                yMax = bottom;
        }
        var allRects = FindObjectsOfType<BoxCollider2D>();
        foreach (var box in allRects)
        {
            var corners = box.GetWorldCorners();
            for (int i = 0; i < corners.Length; i++)
            {
                var v = corners[i];
                if (v.x < xMin)
                    xMin = v.x;
                if (v.x > xMax)
                    xMax = v.x;
                if (v.y < yMin)
                    yMin = v.y;
                if (v.y > yMax)
                    yMax = v.y;
            }
        }
        var allEdges = FindObjectsOfType<EdgeCollider2D>();
        foreach (var edge in allEdges)
        {
            var vertices = edge.GetWorldPoints();
            for (int i = 0; i < vertices.Length; i++)
            {
                var v = vertices[i];
                if (v.x < xMin)
                    xMin = v.x;
                if (v.x > xMax)
                    xMax = v.x;
                if (v.y < yMin)
                    yMin = v.y;
                if (v.y > yMax)
                    yMax = v.y;
            }
        }

        // Move camera in steps and create tile screenshots for exporter
        camera.transform.position = new Vector3(xMin + ((xMax - xMin) * 0.5f), yMin + ((yMax - yMin) * 0.5f), -10.0f);
        camera.backgroundColor = Color.black;
        float widthStep = camera.orthographicSize * 2.0f * camera.aspect;
        float heightStep = camera.orthographicSize * 2.0f;
        camera.transform.position = new Vector3(xMin - widthStep / 2, yMin - heightStep / 2, -10);

        float capturedWidth = xMin;
        float capturedHeight = yMin;
        int row = 0;
        int col = 0;

        int width = (Mathf.CeilToInt((xMax - xMin) / widthStep)) * (int)camera.pixelWidth;
        int height = (Mathf.CeilToInt((yMax - yMin) / heightStep)) * (int)camera.pixelHeight;
        Debug.Log(width + ", " + height);

        // XML file containing information about how to arange tiles to reconstruct scene.
        StringBuilder xmlString = new StringBuilder();
        xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
        xmlString.Append("<Layout xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Name=\"" + Application.loadedLevelName + "\">\n");
        xmlString.Append(string.Format("\t<SceneWidth>{0}</SceneWidth>\n", width));
        xmlString.Append(string.Format("\t<SceneHeight>{0}</SceneHeight>\n", height));
        string folderName = string.Format("Assets/PolyTie/LayoutExports/{0}", Application.loadedLevelName);
        if (Directory.Exists("Assets/PolyTie") == false)
            Directory.CreateDirectory("Assets/PolyTie");
        if (Directory.Exists("Assets/PolyTie/LayoutExports") == false)
            Directory.CreateDirectory("Assets/PolyTie/LayoutExports");
        if (Directory.Exists(folderName) == false)
            Directory.CreateDirectory(folderName);
        if (Directory.Exists(string.Format("{0}/Tiles", folderName)) == false)
            Directory.CreateDirectory(string.Format("{0}/Tiles", folderName));

        while (capturedHeight <= yMax)
        {
            capturedHeight += heightStep;
            camera.transform.position += new Vector3(0, heightStep, 0);
            capturedWidth = xMin;
            col = 0;
            camera.transform.position = new Vector3(xMin - widthStep / 2, camera.transform.position.y, -10);
            while (capturedWidth <= xMax)
            {
                capturedWidth += widthStep;
                camera.transform.position += new Vector3(widthStep, 0, 0);
                string fileName = string.Format("{0}-LayoutTile-{1}-{2}.png", Application.loadedLevelName, row, col);
                string filePath = string.Format("{0}/Tiles/{1}", folderName, fileName);
                if (File.Exists(filePath) == true)
                    File.Delete(filePath);
                yield return new WaitForEndOfFrame();
                Application.CaptureScreenshot(filePath);

                // Add to xml file.
                xmlString.Append(string.Format("\t<Tile Width=\"{0}\" Height=\"{1}\">\n", camera.pixelWidth, camera.pixelHeight));
                xmlString.Append("\t\t<FileName>");
                xmlString.Append(string.Format("Tiles/{0}", fileName));
                xmlString.Append("</FileName>\n");
                xmlString.Append("\t\t<PosX>");
                xmlString.Append(camera.pixelWidth * col);
                xmlString.Append("</PosX>\n");
                xmlString.Append("\t\t<PosY>");
                xmlString.Append(camera.pixelHeight * row);
                xmlString.Append("</PosY>\n");
                xmlString.Append("\t</Tile>\n");

                // Wait for file.
                while (File.Exists(filePath) == false)
                    yield return new WaitForEndOfFrame();

                col++;
            }
            row++;
        }
        xmlString.Append("</Layout>");

        using (StreamWriter writer = new StreamWriter(string.Format("{0}/LayoutImport.xml", folderName), false))
        {
            writer.Write(xmlString.ToString());
        }

        yield return new WaitForEndOfFrame();
        _isShowingGUI = true;
    }
}
