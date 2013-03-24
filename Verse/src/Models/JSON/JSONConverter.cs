﻿using System;
using System.Globalization;

namespace Verse.Models.JSON
{
	static class	JSONConverter
	{
		#region Methods

		public static bool	FromBoolean (JSONWriter writer, bool value)
		{
			writer.WriteBoolean (value);

			return true;
		}

		public static bool	FromChar (JSONWriter writer, char value)
		{
			writer.WriteString (value.ToString (CultureInfo.InvariantCulture));

			return true;
		}

		public static bool	FromFloat4 (JSONWriter writer, float value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromFloat8 (JSONWriter writer, double value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt1s (JSONWriter writer, sbyte value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt1u (JSONWriter writer, byte value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt2s (JSONWriter writer, short value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt2u (JSONWriter writer, ushort value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt4s (JSONWriter writer, int value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt4u (JSONWriter writer, uint value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt8s (JSONWriter writer, long value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromInt8u (JSONWriter writer, ulong value)
		{
			writer.WriteNumber (value);

			return true;
		}

		public static bool	FromString (JSONWriter writer, string value)
		{
			writer.WriteString (value);

			return true;
		}

		public static bool	ToBoolean (JSONLexer lexer, out bool value)
		{
			switch (lexer.Lexem)
			{
				case JSONLexem.False:
					value = false;

					return true;

				case JSONLexem.True:
					value = true;

					return true;

				default:
					value = default (bool);

					return false;
			}
		}

		public static bool	ToChar (JSONLexer lexer, out char value)
        {
        	value = (char)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToFloat4 (JSONLexer lexer, out float value)
        {
        	value = (float)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToFloat8 (JSONLexer lexer, out double value)
        {
        	value = lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt1s (JSONLexer lexer, out sbyte value)
        {
        	value = (sbyte)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt1u (JSONLexer lexer, out byte value)
        {
        	value = (byte)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt2s (JSONLexer lexer, out short value)
        {
        	value = (short)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt2u (JSONLexer lexer, out ushort value)
        {
        	value = (ushort)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt4s (JSONLexer lexer, out int value)
        {
        	value = (int)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt4u (JSONLexer lexer, out uint value)
        {
        	value = (uint)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt8s (JSONLexer lexer, out long value)
        {
        	value = (long)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToInt8u (JSONLexer lexer, out ulong value)
        {
        	value = (ulong)lexer.AsDouble;

			return lexer.Lexem == JSONLexem.Number;
		}

        public static bool	ToString (JSONLexer lexer, out string value)
        {
        	value = lexer.AsString;

			return lexer.Lexem == JSONLexem.Null || lexer.Lexem == JSONLexem.String;
		}
		
		#endregion
	}
}
