const path = require("path");
const fs = require("fs");

let currentDir = process.cwd();

// Args:
// /root:RootDir / Place to put output files
// /t:Type of project running (free-text)
// /r - rerun
// /specsFor

let rerun = false;
let type = "default";
let rootDir = "";
let specsFor = false;

let settings = {
};

process.argv.forEach((item, index) => {
    if (index == 0) return;

    if (item.indexOf("/root:") == 0) {
        rootDir = item.substr(6);
    }
    if (item.indexOf("/type:") == 0) {
        type = item.substr(6);
    } 
    if (item.indexOf("/rerun") == 0) {
        rerun = true;
    } 
    if( item.indexOf("/specsFor") == 0 ) {
        specsFor = true;
    }
});

if (rootDir.length == 0) {
    console.log("Missing rootDir - use /root:<root dir> - the rootdir is where any output files are stored");
    return;
}

let settingsFile = path.join(rootDir, ".buildsettings");
if (fs.existsSync(settingsFile)) {
    let settinsgAsJson = fs.readFileSync(settingsFile, "utf8");
    settings = JSON.parse(settinsgAsJson);
}

let directoryToRun = "";

if (rerun) {
    directoryToRun = settings[type].workingDir;
} else {
    while (currentDir.length > 0) {
        try {
            let content = fs.readdirSync(currentDir);
            let files = content.filter(function (elm) { return elm.match(/.*\.(csproj|sln)/ig); });
            if (files.length == 1) {
                settings[type] = {
                    workingDir:currentDir
                };
                
                break;
            }
        } catch (ex) { }

        currentDir = currentDir.substr(0, currentDir.lastIndexOf(path.sep));
    }

    directoryToRun = currentDir;
}

fs.writeFileSync(settingsFile, JSON.stringify(settings), "utf8");

if( specsFor == true ) {
    directoryToRun = directoryToRun.replace("Source", "Specifications");
}

console.log(directoryToRun);



