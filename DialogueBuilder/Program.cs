﻿// See https://aka.ms/new-console-template for more information

using DialogueDumper;
using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;
using KenshiWikiValidator.Features.DataItemConversion;
using OpenConstructionSet.Data;

if (Directory.Exists("characters"))
{
    Directory.Delete("characters", true);
}

var repository = new ItemRepository();
await repository.Load();

var dialogueBuilder = new DialogueBuilder(repository);
var characters = repository.GetDataItemsByType(ItemType.Character);
var beep = characters.Single(character => character.Name == "Beep");

var dialogueTreeCreator = new DialogueTreeCreator(repository);
var text = dialogueTreeCreator.Create(beep);

var path = Path.Combine("characters", $"{beep.Name}.txt");
Directory.CreateDirectory("characters");
File.WriteAllText(path, text);