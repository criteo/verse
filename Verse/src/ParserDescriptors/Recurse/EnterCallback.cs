﻿
namespace Verse.ParserDescriptors.Recurse
{
	delegate bool EnterCallback<T, C, V> (ref T target, IReader<C, V> reader, C context);
}
