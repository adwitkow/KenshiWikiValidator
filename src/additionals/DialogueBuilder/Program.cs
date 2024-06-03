// See https://aka.ms/new-console-template for more information

using DialogueDumper;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

const string charactersDirectory = "characters";

if (Directory.Exists(charactersDirectory))
{
    Directory.Delete(charactersDirectory, true);
}

var contextProvider = new ContextProvider();
var context = contextProvider.GetDataMiningContext();
var repository = new ItemRepository(context);
repository.Load();

var characters = repository.GetItems<Character>();
var beep = characters.Single(character => character.Name == "Beep");

var dialogueTreeCreator = new DialogueTreeCreator(repository);
var text = dialogueTreeCreator.Create(beep);

var path = Path.Combine(charactersDirectory, $"{beep.Name}.txt");
Directory.CreateDirectory(charactersDirectory);
await File.WriteAllTextAsync(path, text);