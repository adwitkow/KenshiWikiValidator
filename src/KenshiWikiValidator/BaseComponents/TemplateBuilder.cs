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

using System.Text;

namespace KenshiWikiValidator.BaseComponents
{
    public class TemplateBuilder
    {
        public string Build(WikiTemplate template)
        {
            if (string.IsNullOrEmpty(template.Name))
            {
                throw new ArgumentException("Template needs to have a name");
            }

            var builder = new StringBuilder("{{");

            var newlineAfterName = ShouldAddNewlineAfterName(template);

            Append(builder, template.Name, newlineAfterName);

            var newlines = ShouldAddNewlines(template);

            if (!newlineAfterName && newlines)
            {
                builder.Append(' ');
            }

            foreach (var parameter in template.UnnamedParameters)
            {
                if (!newlines)
                {
                    builder.Append(' ');
                }

                Append(builder, $"| {parameter}", newlines);
            }

            var validParameters = template.Parameters.Where(pair => pair.Value is not null && !string.IsNullOrWhiteSpace(pair.Value));

            var maxLength = 0;
            if (validParameters.Any())
            {
                maxLength = validParameters.Max(pair => pair.Key.Length);
            }

            foreach (var pair in validParameters)
            {
                if (!newlines)
                {
                    builder.Append(' ');
                }

                var paddedKey = pair.Key.PadRight(maxLength);
                Append(builder, $"| {paddedKey} = {pair.Value}", newlines);
            }

            builder.Append("}}");

            return builder.ToString();
        }

        private static void Append(StringBuilder builder, string toAppend, bool addNewline)
        {
            if (addNewline)
            {
                builder.AppendLine(toAppend);
            }
            else
            {
                builder.Append(toAppend);
            }
        }

        private static bool ShouldAddNewlineAfterName(WikiTemplate template)
        {
            var noParameters = !template.Parameters.Any()
                && !template.UnnamedParameters.Any();
            if (noParameters)
            {
                return false;
            }

            if (template.UnnamedParameters.Count == 1)
            {
                return false;
            }

            if (template.Format == WikiTemplate.TemplateFormat.Inline)
            {
                return false;
            }

            return true;
        }

        private static bool ShouldAddNewlines(WikiTemplate template)
        {
            if (template.Format == WikiTemplate.TemplateFormat.Block)
            {
                return true;
            }

            if (template.Format == WikiTemplate.TemplateFormat.Inline)
            {
                return false;
            }

            var namedParameterCount = template.Parameters.Count();
            var unnamedParameterCount = template.UnnamedParameters.Count();

            if (namedParameterCount > 3 || unnamedParameterCount > 5)
            {
                return true;
            }

            return false;
        }
    }
}
