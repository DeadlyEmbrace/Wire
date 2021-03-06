﻿using System;
using System.IO;

namespace Wire.ValueSerializers
{
    public class FloatSerializer : SessionAwareValueSerializer<float>
    {
        public const byte Manifest = 12;
        public const int Size = sizeof(float);
        public static readonly FloatSerializer Instance = new FloatSerializer();

        public FloatSerializer() : base(Manifest, () => WriteValueImpl, ()=>ReadValueImpl)
        {
        }

        public static void WriteValueImpl(Stream stream, float f, SerializerSession session)
        {
            var bytes = NoAllocBitConverter.GetBytes(f, session);
            stream.Write(bytes, 0, Size);
        }
        
        public static float ReadValueImpl(Stream stream, DeserializerSession session)
        {
            var buffer = session.GetBuffer(Size);
            stream.Read(buffer, 0, Size);
            return BitConverter.ToSingle(buffer, 0);
        }
    }
}