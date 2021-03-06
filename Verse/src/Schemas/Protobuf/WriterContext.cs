﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using ProtoBuf;

namespace Verse.Schemas.Protobuf
{
    class WriterContext
    {
        #region Property

        public int Position
        {
            get
            {
                long localPosition;

                localPosition = this.subObjectInstances.Count > 0
                    ? this.subObjectInstances.Peek().Stream.Position
                    : 0;

                return (int)(this.parentOffset + localPosition);
            }
        }

        #endregion

        #region attributes

        private int fieldIndex;

        private long parentOffset;

        private readonly Stream stream;

        private readonly Stack<SubObjectInstance> subObjectInstances;

        #endregion

        #region constructor

        public WriterContext(Stream stream)
        {
            this.fieldIndex = 0;
            this.parentOffset = 0;
            this.stream = stream;
            this.subObjectInstances = new Stack<SubObjectInstance>();
        }

        #endregion

        #region Methods

        public void Key(string key)
        {
            this.fieldIndex = int.Parse(key, CultureInfo.InvariantCulture);
        }

        public void ObjectBegin()
        {
            SubObjectInstance subObjectInstance;

            if (this.subObjectInstances.Count > 0)
                this.parentOffset += this.subObjectInstances.Peek().Stream.Position;

            subObjectInstance = new SubObjectInstance(this.fieldIndex);

            this.subObjectInstances.Push(subObjectInstance);
        }

        public bool ObjectEnd()
        {
            SubObjectInstance subObjectInstance;

            if (this.subObjectInstances.Count == 0)
                return false;

            subObjectInstance = this.subObjectInstances.Pop();

            this.parentOffset -= subObjectInstance.Stream.Position;

            this.CopyToParent(subObjectInstance);

            return true;
        }

        public bool Value(Value value)
        {
            ProtoWriter destWriter;

            if (this.subObjectInstances.Count == 0)
                return false;

            destWriter = this.subObjectInstances.Peek().Writer;

            switch (value.Type)
            {
                case ContentType.Double:
                    ProtoWriter.WriteFieldHeader(fieldIndex, WireType.Fixed64, destWriter);
                    ProtoWriter.WriteDouble(value.DoubleContent, destWriter);
                    break;

                case ContentType.Float:
                    ProtoWriter.WriteFieldHeader(fieldIndex, WireType.Fixed32, destWriter);
                    ProtoWriter.WriteSingle(value.FloatContent, destWriter);
                    break;

                case ContentType.Long:
                    ProtoWriter.WriteFieldHeader(fieldIndex, WireType.Variant, destWriter);
                    ProtoWriter.WriteInt64(value.LongContent, destWriter);
                    break;

                case ContentType.String:
                    ProtoWriter.WriteFieldHeader(fieldIndex, WireType.String, destWriter);
                    ProtoWriter.WriteString(value.StringContent ?? string.Empty, destWriter);
                    break;
            }

            return true;
        }

        private void CopyToParent(SubObjectInstance subObjectInstance)
        {
            subObjectInstance.Writer.Close();
            subObjectInstance.Stream.Seek(0, SeekOrigin.Begin);

            if (this.subObjectInstances.Count == 0)
            {
                subObjectInstance.Stream.CopyTo(this.stream);
            }
            else
            {
                ProtoWriter destWriter;

                destWriter = this.subObjectInstances.Peek().Writer;

                ProtoWriter.WriteFieldHeader(subObjectInstance.Index, WireType.String, destWriter);
                ProtoWriter.WriteString(Encoding.UTF8.GetString(subObjectInstance.Stream.ToArray()), destWriter);
            }
        }

        #endregion
    }
}
