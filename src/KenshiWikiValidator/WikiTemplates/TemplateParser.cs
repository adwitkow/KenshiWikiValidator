// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
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

namespace KenshiWikiValidator.WikiTemplates
{
    public class TemplateParser
    {
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

            var templateElements = trimmed.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(element => element.Trim())
                .ToList();
            var name = templateElements.First();

            var properties = new SortedList<string, string?>();
            for (int i = 1; i < templateElements.Count; i++)
            {
                var element = templateElements[i];
                if (element.Contains('='))
                {
                    var splitElements = element.Split('=');
                    var key = splitElements[0].Trim();
                    var value = splitElements[1].Trim();

                    properties.Add(key, value);
                }
                else
                {
                    properties.Add(i.ToString(), element);
                }
            }

            var result = new WikiTemplate(name, properties);

            return result;
        }
    }
}