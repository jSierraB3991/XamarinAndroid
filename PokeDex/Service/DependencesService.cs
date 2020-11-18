namespace PokeDex.Service
{
    public class DependencesService
    {
        public static DependencesService INSTANCE;

        public IPokeDexApiService PokeDexApiService { get; set; }

        public IPokeDexLocalService PokeDexLocalService { get; set; }

        public DependencesService()
        {
            INSTANCE = this;
            this.PokeDexApiService = new PokeDexApiService();
            this.PokeDexLocalService = new PokeDexLocalService();
        }
    }
}