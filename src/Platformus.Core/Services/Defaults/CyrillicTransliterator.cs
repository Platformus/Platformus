// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults;

public class CyrillicTransliterator : ITransliterator
{
  // This is NOT correct cyrillic transliteration, it can only be used for quick things like
  // URL or filename sanitizing
  public string Transliterate(string text)
  {
    Dictionary<char, string> letters = new Dictionary<char, string> {
      { 'А', "A" }, { 'Б', "B" }, { 'В', "V" }, { 'Г', "G" }, { 'Ґ', "G" },
      { 'Д', "D" }, { 'Е', "E" }, { 'Ё', "Yo" }, { 'Є', "E" }, { 'Ж', "Zh" },
      { 'З', "Z" }, { 'И', "I" }, { 'Й', "J" }, { 'І', "I" }, { 'Ї', "I" },
      { 'К', "K" }, { 'Л', "L" }, { 'М', "M" }, { 'Н', "N" }, { 'О', "O" },
      { 'П', "P" }, { 'Р', "R" }, { 'С', "S" }, { 'Т', "T" }, { 'У', "U" },
      { 'Ф', "F" }, { 'Х', "Kh" }, { 'Ц', "C" }, { 'Ч', "Ch" }, { 'Ш', "Sh" },
      { 'Щ', "Shch" }, { 'Ы', "Y" }, { 'Э', "E" }, { 'Ю', "Yu" }, { 'Я', "Ya" },
      { 'а', "a" }, { 'б', "b" }, { 'в', "v" }, { 'г', "g" }, { 'ґ', "g" },
      { 'д', "d" }, { 'е', "e" }, { 'ё', "yo" }, { 'є', "e" }, { 'ж', "zh" },
      { 'з', "z" }, { 'и', "i" }, { 'й', "j" }, { 'і', "i" }, { 'ї', "i" },
      { 'к', "k" }, { 'л', "l" }, { 'м', "m" }, { 'н', "n" }, { 'о', "o" },
      { 'п', "p" }, { 'р', "r" }, { 'с', "s" }, { 'т', "t" }, { 'у', "u" },
      { 'ф', "f" }, { 'х', "kh" }, { 'ц', "c" }, { 'ч', "ch" }, { 'ш', "sh" },
      { 'щ', "shch" }, { 'ы', "y" }, { 'э', "e" }, { 'ю', "yu" }, { 'я', "ya" }
    };

    StringBuilder sb = new StringBuilder(text);

    foreach (KeyValuePair<char, string> letter in letters)
      sb.Replace(letter.Key.ToString(), letter.Value);

    return sb.ToString();
  }
}