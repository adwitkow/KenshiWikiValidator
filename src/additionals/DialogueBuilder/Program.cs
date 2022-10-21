// See https://aka.ms/new-console-template for more information

using DialogueDumper;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

if (Directory.Exists("characters"))
{
    Directory.Delete("characters", true);
}

var repository = new ItemRepository();
repository.Load();

var characters = repository.GetItems<Character>();
var beep = characters.Single(character => character.Name == "Beep");
var beepLosesArm = repository.GetItems<Dialogue>().FirstOrDefault(d => d.StringId == "96393-rebirth.mod");

var dialogueTreeCreator = new DialogueTreeCreator(repository);
var text = dialogueTreeCreator.Create(beep);
//var text = dialogueTreeCreator.CreateDialogue(beep, beepLosesArm);

var path = Path.Combine("characters", $"{beep.Name}.txt");
Directory.CreateDirectory("characters");
File.WriteAllText(path, text);