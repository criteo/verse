namespace Verse.ParserDescriptors.Flat
{
    internal interface INode<TEntity, TContext, TNative>
    {
        #region Properties

        bool CanAssign
        {
            get;
        }

        #endregion

        #region Methods

        void Assign(ref TEntity target, TNative value);

        bool Enter(ref TEntity target, IReader<TContext, TNative> reader, TContext context);

        INode<TEntity, TContext, TNative> Follow(char c);

        #endregion
    }
}