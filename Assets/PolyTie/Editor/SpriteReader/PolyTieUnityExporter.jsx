//------------------------------------------------------------------------------//
// Poly Tie Unity Exporter
// Exoprts the layers of a photoshop file as a set of png files
// along with a xml file containing position information of
// the level. Gruped layers in photoshop are placed inside 
// a layer within Unity with the same name as the group
//------------------------------------------------------------------------------//

var _exportPath = "";
var _xmlString =  "";
var _file;

var _oldRulerUnits;
var _oldTypeUnits;
var _idx = 0;

function removeEmptyGroups(data) {
    var foundEmpty = true;
    
    for (var i = data.layerSets.length - 1; 0 <= i; i--) {
        if(removeEmptyGroups(data.layerSets[i])) {
            data.layerSets[i].remove();
        }
        else {
			foundEmpty = false;
        }
    }
	if(data.artLayers.length > 0) {
		foundEmpty = false;
	}
	return foundEmpty;
}

function removeHiddenLayers(data) {
    for (var i = 0; i < data.artLayers.length; i++) {
		if (data.artLayers[i].visible == false) {
                data.artLayers[i].allLocked = false;
                data.artLayers[i].remove();
         }
	}

	for (var i = 0; i < data.layerSets.length; i++) {
		removeHiddenLayers(data.layerSets[i]);
	}
}

function hideAllLayers(data) {
    for (var i = 0; i < data.layerSets.length; i++) {
        hideAllLayers(data.layerSets[i]);
    }
    
    for (var i = 0; i < data.artLayers.length; i++) {
        data.artLayers[i].allLocked = false;
        data.artLayers[i].visible = false;
    }
}

// Exporting each layer as a cropped png image and adding meta data to xml file
function exportPngs(data) {
    for (var i = 0; i < data.layerSets.length ; i++) {
        exportPngs(data.layerSets[i]);
    }
    
    if (data.typename != "LayerSet") {
        return;
    }

    _xmlString += "\t\t<Layer Name=\""+ data.name + "\"> \n";
    _xmlString += "\t\t\t<Sprites> \n";
    for (var i = 0; i < data.artLayers.length ; i++) {
        data.artLayers[i].visible = true;
        saveCropPng(_file.duplicate(), (data.artLayers[i].name.replace(/ /g,"-") + "-layer-" + _idx + "-" + i), data.artLayers[i].name);
        data.artLayers[i].visible = false;
    }
    _xmlString += "\t\t\t</Sprites> \n";
    _xmlString += "\t\t</Layer>\n";
    _idx++;
}

// Crop transparent pixels and save visible layers as png
function saveCropPng(file, fileName, imageName, xStartOffset, yStartOffset, noMerge) {
    xStartOffset = typeof xStartOffset !== 'undefined' ? xStartOffset : 0;
    yStartOffset = typeof yStartOffset !== 'undefined' ? yStartOffset : 0;

    if (!noMerge) {
        file.mergeVisibleLayers();
    }
    
    // Get image dimensions
    var width = file.width.value;
    var height = file.height.value;
    
    // Get rid of floating pixels.
    /*try {
        selectLayerContent();
        file.selection.contract(12);
        file.selection.expand(24);
        file.selection.invert();
        file.selection.clear();
    }
    catch(e) {
    }*/

    // Trim top and left of image
    file.trim(TrimType.TRANSPARENT, true, true, false, false);    
    var xOffset = width - file.width.value + xStartOffset;
    var yOffset = height - file.height.value + yStartOffset;
    
    file.trim(TrimType.TRANSPARENT);
    
    // Figure out how many tiles we have to save in case it exceeds 4096 maximum
    var xTileCount = Math.ceil(file.width.value / 4096);
    var yTileCount = Math.ceil(file.height.value / 4096);
    
    if (xTileCount == 1 && yTileCount == 1) {
        saveOutToPng(file, fileName, imageName, width, height, xOffset, yOffset);
    }
    else {  // Image is to big - crop it in tiles
        var tileWidth = file.width.value / xTileCount;
        var tileHeight = file.height.value / yTileCount;
        
        for (var i = 0; i < xTileCount; i++) {
            for (var j = 0; j < yTileCount; j++) {
                var fileCrop = file.duplicate();
                fileCrop.crop([tileWidth * i, tileHeight * j, tileWidth * (i + 1), tileHeight * (j + 1)]);
                saveOutToPng(fileCrop, fileName + "-q" + i.toString() + j.toString(), imageName + "-q" + i.toString() + j.toString(), width, height, xOffset + (tileWidth * i), yOffset + (tileHeight * j));
            }
        }
        file.close(SaveOptions.DONOTSAVECHANGES);
    }
}

function saveOutToPng(file, fileName, imageName, width, height, xOffset, yOffset) {    
    // Extend to next power of two
    var newWidth = file.width.value;
    var newHeight = file.height.value;

    var nexPow2Width = nextPowerOfTwo(newWidth);
    var nexPow2Height = nextPowerOfTwo(newHeight);
    
    file.resizeCanvas(nexPow2Width, nexPow2Height);
    
    // Adjust offset
    xOffset -= (nexPow2Width - newWidth) * 0.5;
    yOffset -= (nexPow2Height - newHeight) * 0.5;
    
    // Calculate position within photoshop file after croping
    var xPos = xOffset + (nexPow2Width * 0.5);
    var yPos = yOffset + (nexPow2Height * 0.5);
    
    // Transform to coordinate system with origin in center
    xPos -= (width * 0.5);
    yPos = -(yPos - (height * 0.5));
    
    var pngFile = new File(_exportPath + "/Sprites/" + fileName + ".png");
    var options = new PNGSaveOptions();
    
	file.saveAs(pngFile, options, true, Extension.LOWERCASE);
	file.close(SaveOptions.DONOTSAVECHANGES);
    
    _xmlString += "\t\t\t\t<Sprite Name=\""+ imageName + "\" Width=\"" + nexPow2Width + "\" Height=\"" + nexPow2Height + "\">\n";
    _xmlString += "\t\t\t\t\t<FileName>Sprites/" + fileName + ".png</FileName>\n";
    _xmlString += "\t\t\t\t\t<x>" + xPos+ "</x>\n";
    _xmlString += "\t\t\t\t\t<y>" + yPos+ "</y>\n";
    _xmlString += "\t\t\t\t</Sprite>\n";
}

function nextPowerOfTwo(value) {
    value--;
    value |= value >> 1;
    value |= value >> 2;
    value |= value >> 4;
    value |= value >> 8;
    value |= value >> 16;
    value++;
    
    return value;
 }

function selectLayerContent() {
    if(app.activeDocument.activeLayer.isBackgroundLayer) {
        return;// background doesn't have a transparency mask
    }
    var desc = new ActionDescriptor();
    var ref = new ActionReference();
    ref.putProperty( charIDToTypeID( "Chnl" ), charIDToTypeID( "fsel" ) );
    desc.putReference( charIDToTypeID( "null" ), ref );
    var ref1 = new ActionReference();
    ref1.putEnumerated( charIDToTypeID( "Chnl" ), charIDToTypeID( "Chnl" ), charIDToTypeID( "Trsp" ) );
    desc.putReference( charIDToTypeID( "T   " ), ref1 );
    executeAction( charIDToTypeID( "setd" ), desc, DialogModes.NO );
}

selectLayerContent ();

function main() {
    // Valdate current document.
    if (app.documents.length <= 0) {
        if (app.playbackDisplayDialogs != DialogModes.NO) {
            alert("No open document found to export");
        }
        return 'cancel';
    }
   
    // Get export path
    var defaultPath = app.activeDocument.path;
    var defaultSettingsPath = new File(app.preferencesFolder + "/PolyTieTmp/lastPath.txt");
    if (defaultSettingsPath.exists) {
        defaultSettingsPath.open('r');
        defaultPath = defaultSettingsPath.readln();
        defaultSettingsPath.close();
    }
    _exportPath = Folder(defaultPath).selectDlg("Export to: ");
    
    if (!_exportPath) {
        return;
    }
    
    // Cache old units
    _oldRulerUnits = app.preferences.rulerUnits;
    _oldTypeUnits = app.preferences.typeUnits;
    
	app.preferences.rulerUnits = Units.PIXELS;
	app.preferences.typeUnits = TypeUnits.PIXELS;
    
    var originalFileName = app.activeDocument.name;
    originalFileName = originalFileName.slice(0, -4);
    _file = app.activeDocument.duplicate();    
	_file.activeLayer = _file.layers[_file.layers.length-1];
    
	for(var i = 0; i < _file.artLayers.length; i++) {
		_file.artLayers[i].remove();
	}
    removeHiddenLayers(_file);
    removeEmptyGroups(_file);
    hideAllLayers(_file);
    
    // Create sprite folder
    var spriteFolder = Folder(_exportPath + "/Sprites");
    if (!spriteFolder.exists) spriteFolder.create();
    
    // Set up xml file
    _xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
    _xmlString += "<Level xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Name=\"" + originalFileName + "\">\n"
    _xmlString += "\t<Layers>\n";
    exportPngs(_file);
	_xmlString += "\t</Layers>\n";
	_xmlString += "</Level>\n";
    
    _file.close(SaveOptions.DONOTSAVECHANGES);

    // Write out xml data
    var xmlFile = new File(_exportPath + "/" + originalFileName + ".xml");
    xmlFile.open('w');
    xmlFile.writeln(_xmlString);
    xmlFile.close();
    
    // Save last path
    var polyTieSettingsFolder = new Folder(app.preferencesFolder + "/PolyTieTmp");
    if (!polyTieSettingsFolder.exists) polyTieSettingsFolder.create();
    var lastPath = new File(app.preferencesFolder + "/PolyTieTmp/lastPath.txt");
    lastPath.open('w');
    lastPath.writeln(_exportPath);
    lastPath.close();
    
    // Reset to old units
	app.preferences.rulerUnits = _oldRulerUnits;
	app.preferences.typeUnits = _oldTypeUnits;
}

// Run script.
main();