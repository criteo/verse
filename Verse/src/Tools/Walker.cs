using System;
using System.Collections;
using System.Collections.Generic;

namespace Verse.Tools
{
    internal class Walker<T> : IEnumerable<T>
    {
        #region Attributes

        private readonly IEnumerator<T> enumerator;

        #endregion

        #region Constructors

        public Walker(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        #endregion

        #region Methods / Public

        public IEnumerator<T> GetEnumerator()
        {
            return this.enumerator;
        }

        #endregion

        #region Methods / Explicit

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}