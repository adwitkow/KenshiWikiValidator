﻿// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Text.RegularExpressions;

namespace KenshiWikiValidator.BaseComponents
{
    public class TemplateParser
    {
        private static readonly Regex PipeRegex = new Regex(@"(?<pipe>\|)|(\[\[.+?\|?.+?\]\])");

        private int lastPipeIndex = 0;

        public IEnumerable<WikiTemplate> ParseAllTemplates(string content)
        {
            var templates = new List<WikiTemplate>();

            var startingIndex = content.IndexOf("{{");
            var nextStartingIndex = content.IndexOf("{{", startingIndex + 1);
            var endingIndex = content.IndexOf("}}");

            while (nextStartingIndex < endingIndex)
            {
                nextStartingIndex = content.IndexOf("{{", nextStartingIndex + 1);
                endingIndex = content.IndexOf("}}", endingIndex + 1);
            }

            while (startingIndex != -1 && endingIndex != -1)
            {
                var body = content.Substring(startingIndex, endingIndex - startingIndex + 2);
                templates.Add(this.Parse(body));

                startingIndex = content.IndexOf("{{", endingIndex);
                if (startingIndex != -1)
                {
                    endingIndex = content.IndexOf("}}", startingIndex);
                }
            }

            return templates;
        }

        public WikiTemplate Parse(string input)
        {
            var trimmed = input.Trim();
            if (!trimmed.StartsWith("{{"))
            {
                throw new ArgumentException("Input must start with a double brace ('{{')", nameof(input));
            }

            if (!trimmed.EndsWith("}}"))
            {
                throw new ArgumentException("Input must end with a double brace ('{{')", nameof(input));
            }

            trimmed = input.Substring(2, trimmed.Length - 4);

            if (string.IsNullOrEmpty(trimmed))
            {
                throw new ArgumentException("Input does not have any content to parse.", nameof(input));
            }

            var elements = this.SplitOnPipes(trimmed);

            var name = trimmed.Substring(0, this.lastPipeIndex).Trim();
            var result = CreateWikiTemplate(name, elements);

            return result;
        }

        private static WikiTemplate CreateWikiTemplate(string name, IList<string> elements)
        {
            var namedProperties = new List<KeyValuePair<string, string?>>();
            var unnamedProperties = new List<string>();
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                if (element.Contains('='))
                {
                    var splitElements = element.Split('=');
                    var key = splitElements[0].Trim();
                    var value = splitElements[1].Trim();

                    namedProperties.Add(new KeyValuePair<string, string?>(key, value));
                }
                else
                {
                    unnamedProperties.Add(element);
                }
            }

            namedProperties.Reverse();

            return new WikiTemplate(
                name,
                unnamedProperties,
                new IndexedDictionary<string, string?>(namedProperties));
        }

        private IList<string> SplitOnPipes(string trimmed)
        {
            var matches = PipeRegex.Matches(trimmed);
            var elements = new List<string>();
            this.lastPipeIndex = trimmed.Length;
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var group = match.Groups["pipe"];

                if (!group.Success)
                {
                    continue;
                }

                var index = group.Index;
                var element = trimmed.Substring(index + 1, this.lastPipeIndex - index - 1);
                this.lastPipeIndex = index;

                elements.Add(element.Trim());
            }

            return elements;
        }
    }
}