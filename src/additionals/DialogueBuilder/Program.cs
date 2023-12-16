// See https://aka.ms/new-console-template for more information

using DialogueDumper;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

if (Directory.Exists("characters"))
{
    Directory.Delete("characters", true);
}

Directory.CreateDirectory("characters");

var repository = new ItemRepository();
repository.Load();

var dialogueTreeCreator = new DialogueTreeCreator(repository);

var characters = repository.GetItems<Character>();
foreach (var character in characters)
{
    var dialogueContent = dialogueTreeCreator.Create(character);
    var path = Path.Combine("characters", $"{character.Name} ({character.StringId}).txt");

    File.WriteAllText(path, dialogueContent);
}