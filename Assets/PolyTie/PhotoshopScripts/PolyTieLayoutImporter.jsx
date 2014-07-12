function main() {
    var importFile = File.openDialog("Select layout xml file", "XML:*.xml", false);
    
    if (importFile) {
        importFile.open('r');
        var xmlString = importFile.read();
        var layoutXML = new XML(xmlString);
        var width = parseInt(layoutXML.SceneWidth);
        var height = parseInt(layoutXML.SceneHeight);
        
        // Create a document of required size
        var mainDoc = documents.add(width, height, 72, "Layout", NewDocumentMode.RGB);
        for (var i = 0; i < layoutXML.Tile.length(); i++) {
            var tile = layoutXML.Tile[i];
            var tileFile = new File(importFile.path + "/" + tile.FileName[0]);
            open(tileFile);
            app.activeDocument.selection.selectAll();
            app.activeDocument.selection.copy();
            app.activeDocument.close(SaveOptions.DONOTSAVECHANGES);
                
            // position selection and paste tile in place
            var xPos = parseInt(tile.PosX[0]);
            var yPos = parseInt(tile.PosY[0]);
            var tileW = parseInt(tile.@Width);
            var tileH = parseInt(tile.@Height);
            var selRegion = Array(Array(xPos, height - yPos - tileH),
                                              Array(xPos + tileW, height - yPos - tileH),
                                              Array(xPos + tileW, height - yPos),
                                              Array(xPos, height - yPos));
            mainDoc.selection.select(selRegion);
            mainDoc.paste();
        }
        mainDoc.flatten();
        importFile.close();
    }
    else {
        alert("Could not open specified file");
    }
}

// Run script.
main();