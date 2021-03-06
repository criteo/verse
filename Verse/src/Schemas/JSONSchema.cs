using System;
using System.Text;
using Verse.PrinterDescriptors;
using Verse.ParserDescriptors;
using Verse.Schemas.JSON;

namespace Verse.Schemas
{
    /// <summary>
    /// Schema implementation using JSON format.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class JSONSchema<TEntity> : AbstractSchema<TEntity>
    {
        #region Properties

        /// <inheritdoc/>
        public override IPrinterDescriptor<TEntity> PrinterDescriptor
        {
            get
            {
                return this.printerDescriptor;
            }
        }

        /// <inheritdoc/>
        public override IParserDescriptor<TEntity> ParserDescriptor
        {
            get
            {
                return this.parserDescriptor;
            }
        }

        #endregion

        #region Attributes

        private readonly Encoding encoding;

        private readonly RecurseParserDescriptor<TEntity, ReaderContext, Value> parserDescriptor;

        private readonly RecursePrinterDescriptor<TEntity, WriterContext, Value> printerDescriptor;

        private readonly ValueDecoder valueDecoder;

        private readonly ValueEncoder valueEncoder;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new JSON schema using given text encoding.
        /// </summary>
        /// <param name="encoding">Text encoding</param>
        public JSONSchema(Encoding encoding)
        {
            ValueDecoder decoder;
            ValueEncoder encoder;

            decoder = new ValueDecoder();
            encoder = new ValueEncoder();

            this.encoding = encoding;
            this.parserDescriptor = new RecurseParserDescriptor<TEntity, ReaderContext, Value>(decoder);
            this.printerDescriptor = new RecursePrinterDescriptor<TEntity, WriterContext, Value>(encoder);
            this.valueDecoder = decoder;
            this.valueEncoder = encoder;
        }

        /// <summary>
        /// Create JSON schema using default UTF8 encoding.
        /// </summary>
        public JSONSchema()
            :
                this(new UTF8Encoding(false))
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override IParser<TEntity> CreateParser()
        {
            return this.parserDescriptor.CreateParser(new Reader(this.encoding));
        }

        /// <inheritdoc/>
        public override IPrinter<TEntity> CreatePrinter()
        {
            return this.printerDescriptor.CreatePrinter(new Writer(this.encoding));
        }

        /// <summary>
        /// Declare decoder to convert JSON native value into target output type.
        /// </summary>
        /// <typeparam name="TOutput">Target output type</typeparam>
        /// <param name="converter">Converter from JSON native value to output type</param>
        public void SetDecoder<TOutput>(Converter<Value, TOutput> converter)
        {
            this.valueDecoder.Set(converter);
        }

        /// <summary>
        /// Declare encoder to convert target input type into JSON native value.
        /// </summary>
        /// <typeparam name="TInput">Target input type</typeparam>
        /// <param name="converter">Converter from input type to JSON native value</param>
        public void SetEncoder<TInput>(Converter<TInput, Value> converter)
        {
            this.valueEncoder.Set(converter);
        }

        #endregion
    }
}