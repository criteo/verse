﻿using System;
using Verse.ParserDescriptors;
using Verse.PrinterDescriptors;
using Verse.Schemas.Protobuf;

namespace Verse.Schemas
{
    public class ProtobufSchema<TEntity> : AbstractSchema<TEntity>
    {
        #region Properties

        public override IPrinterDescriptor<TEntity> PrinterDescriptor
        {
            get
            {
                return this.printerDescriptor;
            }
        }

        public override IParserDescriptor<TEntity> ParserDescriptor
        {
            get
            {
                return this.parserDescriptor;
            }
        }

        #endregion

        #region Attributes

        private readonly RecurseParserDescriptor<TEntity, ReaderContext, Value> parserDescriptor;

        private readonly RecursePrinterDescriptor<TEntity, WriterContext, Value> printerDescriptor;

        private readonly ValueDecoder valueDecoder;

        private readonly ValueEncoder valueEncoder;

        #endregion

        #region Constructor

        public ProtobufSchema()
        {
            this.valueDecoder = new ValueDecoder();
            this.valueEncoder = new ValueEncoder();

            this.parserDescriptor = new RecurseParserDescriptor<TEntity, ReaderContext, Value>(this.valueDecoder);
            this.printerDescriptor = new RecursePrinterDescriptor<TEntity, WriterContext, Value>(this.valueEncoder);
        }

        #endregion

        #region Methods / Public

        public override IParser<TEntity> CreateParser()
        {
            return this.parserDescriptor.CreateParser(new Reader());
        }

        public override IPrinter<TEntity> CreatePrinter()
        {
            return this.printerDescriptor.CreatePrinter(new Writer());
        }

        public void SetDecoder<U>(Converter<Value, U> converter)
        {
            this.valueDecoder.Set(converter);
        }

        public void SetEncoder<U>(Converter<U, Value> converter)
        {
            this.valueEncoder.Set(converter);
        }

        #endregion
    }
}
