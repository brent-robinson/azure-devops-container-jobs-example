namespace MyProject.Business
{
    public interface IFavouriteColourCache
    {
        string RetrieveFavouriteColour(string userName);

        void StoreFavouriteColour(string userName, string colour);
    }
}
